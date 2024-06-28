import React, { useState, useEffect } from 'react';
import { Modal, Button, Form, Row, Col, Alert } from 'react-bootstrap';
import '../styles/OrderModal.css';
import FakePaymentForm from './OnlinePayment/FakePaymentForm';

function OrderModal({ isOpen, onClose, storeId, userId, groupedItems }) {
    const [deliveryOptions, setDeliveryOptions] = useState([]);
    const [paymentTypes, setPaymentTypes] = useState([]);
    const [selectedDelivery, setSelectedDelivery] = useState(null);
    const [selectedPaymentType, setSelectedPaymentType] = useState(null);
    const [productsCost, setProductsCost] = useState(0);
    const [deliveryCost, setDeliveryCost] = useState(0);
    const [paymentCommission, setPaymentCommission] = useState(0);
    const [totalCost, setTotalCost] = useState(0);
    const [deliveryError, setDeliveryError] = useState('');
    const [paymentError, setPaymentError] = useState('');
    const [orderError, setOrderError] = useState('');
    const [isOnlinePaymentOpen, setIsOnlinePaymentOpen] = useState(false);

    useEffect(() => {
        const fetchDeliveryOptions = async () => {
            try {
                const response = await fetch(` http://sline.site/api/Order/Delivery/${storeId}`);
                if (response.ok) {
                    const data = await response.json();
                    setDeliveryOptions(data);
                } else {
                    console.error(`Ошибка ${response.status}: ${response.statusText}`);
                }
            } catch (error) {
                console.error('Error loading delivery options:', error);
            }
        };

        const fetchPaymentTypes = async () => {
            try {
                const response = await fetch(' http://sline.site/api/Order/PaymentType');
                if (response.ok) {
                    const data = await response.json();
                    setPaymentTypes([...data]);
                } else {
                    console.error(`Ошибка ${response.status}: ${response.statusText}`);
                }
            } catch (error) {
                console.error('Error loading payment types:', error);
            }
        };

        fetchDeliveryOptions();
        fetchPaymentTypes();
    }, [storeId]);

    useEffect(() => {
        calculateProductsCost();
    }, [groupedItems, storeId]);

    const calculateProductsCost = () => {
        let storeCost = 0;

        const itemsGroup = groupedItems[storeId];
        if (itemsGroup) {
            for (const item of itemsGroup.items) {
                storeCost += item.price * item.quantity;
            }
        }

        setProductsCost(storeCost.toFixed(2)); 
    };

    useEffect(() => {
        calculateTotalCost();
    }, [productsCost, deliveryCost, paymentCommission]);

    const calculateTotalCost = () => {
        const totalCostWithFees = parseFloat(productsCost) + parseFloat(deliveryCost) + parseFloat(paymentCommission);
        setTotalCost(totalCostWithFees.toFixed(2));
    };

    const handleDeliveryChange = (event) => {
        setDeliveryError('');
        const selectedOption = deliveryOptions.find(option => option.deliveryId === parseInt(event.target.value));

        if (selectedOption) {
            setSelectedDelivery(selectedOption);
            setDeliveryCost(selectedOption.deliveryPrice);
        } else {
            setDeliveryError('Пожалуйста, выберите вариант доставки');
        }
    };

    const handlePaymentTypeChange = (event) => {
        setPaymentError('');
        const selectedType = paymentTypes.find(type => type.id === parseInt(event.target.value));

        if (selectedType) {
            setSelectedPaymentType(selectedType);
            setPaymentCommission(selectedType.commission);
        } else {
            setPaymentError('Пожалуйста, выберите тип оплаты');
        }
    };

    const handleOrderClick = async () => {
        if (!selectedDelivery) {
            setDeliveryError('Пожалуйста, выберите вариант доставки');
            return;
        }

        if (!selectedPaymentType) {
            setPaymentError('Пожалуйста, выберите тип оплаты');
            return;
        }

        if (selectedPaymentType.type === 'Онлайн оплата') {
            setIsOnlinePaymentOpen(true);
        } else {
            await createOrder();
        }
    };

    const createOrder = async () => {
        const orderData = {
            userId,
            storeId: parseInt(storeId),
            deliveryId: selectedDelivery ? selectedDelivery.deliveryId : null,
            paymentId: selectedPaymentType ? selectedPaymentType.id : null,
            statusId: 1,
        };

        try {
            const response = await fetch(' http://sline.site/api/Order/Create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                },
                body: JSON.stringify(orderData),
            });

            if (response.ok) {
                onClose();
                window.location.reload();
            } else {
                const errorText = await response.text();
                setOrderError(`Ошибка создания заказа: ${response.statusText}`);
            }
        } catch (error) {
            console.error('Order creation error:', error);
        }
    };

    return (
        <>
            <Modal show={isOpen} onHide={onClose} centered size="lg">
                <Modal.Header closeButton>
                    <Modal.Title>
                        <div className='modal-parametrs-order'>
                            Параметры заказа    
                        </div>
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Row className='mt-3'>
                        <Col md={6}>
                            <Form.Group controlId="deliverySelect">
                                <Form.Label className="indent-text mt-2"></Form.Label>
                                    <Form.Control
                                        as="select"
                                        value={selectedDelivery ? selectedDelivery.deliveryId : ''}
                                        onChange={handleDeliveryChange}
                                        className="indent-text"
                                    >
                                        <option value="">Вариант доставки</option>
                                        {deliveryOptions.map(option => (
                                            <option key={option.deliveryId} value={option.deliveryId}>
                                                {`${option.deliveryType} (${option.deliveryTime} мин) - ${option.deliveryPrice.toFixed(2)} р.`}
                                            </option>
                                        ))}
                                    </Form.Control>
                                    {deliveryError && (
                                        <Alert variant="danger" className="mt-2">
                                            {deliveryError}
                                        </Alert>
                                    )}
                            </Form.Group>

                            <Form.Group controlId="paymentTypeSelect">
                                <Form.Label className="indent-text mt-2"></Form.Label>
                                <Form.Control
                                    as="select"
                                    value={selectedPaymentType ? selectedPaymentType.id : ''}
                                    onChange={handlePaymentTypeChange}
                                >
                                    <option value="">Вариант оплаты</option>
                                    {paymentTypes.map(type => (
                                        <option key={type.id} value={type.id}>
                                            {`${type.type} - Комиссия: ${type.commission.toFixed(2)} р.`}
                                        </option>
                                    ))}
                                </Form.Control>
                                {paymentError && (
                                    <Alert variant="danger" className="mt-2">
                                        {paymentError}
                                    </Alert>
                                )}
                            </Form.Group>
                        </Col>
                        <div className="divider-ordermodal"></div>
                        <Col md={6} className="left-col">
                            <p>Стоимость товаров: {productsCost} р.</p>
                            <p>Стоимость доставки: {deliveryCost.toFixed(2)} р.</p>
                            <p>Комиссия за оплату: {paymentCommission.toFixed(2)} р.</p>
                            <h5>Итог: {totalCost} р.</h5>
                        </Col>
                    </Row>
                    {orderError && (
                        <Alert variant="danger" className="mt-2">
                            {orderError}
                        </Alert>
                    )}
                </Modal.Body>
                    <Button className='button-confirm-order' variant="primary" onClick={handleOrderClick}>
                        Оформить заказ
                    </Button>
            </Modal>

            <FakePaymentForm
                isOpen={isOnlinePaymentOpen}
                totalCost={totalCost}
                onClose={() => setIsOnlinePaymentOpen(false)}
                createOrder={createOrder}
            />
        </>
    );
}

export default OrderModal;