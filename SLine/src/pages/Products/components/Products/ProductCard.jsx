import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import NavbarAuthModal from '../../../../components/Navbar/NavbarAuthModal';
import AvailableStoresModal from '../../../Basket/components/CreateBasket/AvailableStoresModal';
import YandexMapModal from '../../../Profile/components/ModalMap/YandexMapModal';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../../styles/ProductsContainer.css';

function ProductCard({ product }) {
    const [isModalOpen, setModalOpen] = useState(false);
    const [showAuthModal, setShowAuthModal] = useState(false);
    const [stores, setStores] = useState([]);
    const [priceRange, setPriceRange] = useState({ minPrice: null, maxPrice: null });
    const [isInCart, setIsInCart] = useState(false);
    const [isMapModalOpen, setMapModalOpen] = useState(false);
    const [hasAddress, setHasAddress] = useState(false);

    const checkUserAddress = async () => {
        const user = JSON.parse(localStorage.getItem('user'));
        if (!user) {
            return;
        }

        try {
            const response = await fetch(`http://109.107.189.127/api/Profile/GetAllInfo?userId=${user.id}`);
            if (response.ok) {
                const data = await response.json();
                setHasAddress(!!data.address);
            } else {
                console.error('Error checking for the presence of an address:', response.status);
            }
        } catch (error) {
            console.error('Error checking for the presence of an address:', error);
        }
    };

    useEffect(() => {
        checkUserAddress();
    }, []);

    const handleShowAuthModal = () => {
        setShowAuthModal(true);
    };

    const handleAddToCart = async (event) => {
        event.stopPropagation();
        const user = JSON.parse(localStorage.getItem('user'));
        
        if (user) {
            if (hasAddress) {
                fetchStores();
                setModalOpen(true);
                setIsInCart(true);
            } else {
                setMapModalOpen(true);
            }
        } else {
            handleShowAuthModal();
        }
    };

    const fetchStores = async () => {
        try {
            const response = await fetch(`http://109.107.189.127/api/Basket/AvailableStores/${product.productId}`);
            if (response.ok) {
                const data = await response.json();
                setStores(data);
            } else {
                console.error('Error when receiving stores', response.status);
            }
        } catch (error) {
            console.error('Error when receiving stores:', error);
        }
    };

    useEffect(() => {
        const fetchPriceRange = async () => {
            try {
                const response = await fetch(`http://109.107.189.127/api/Catalog/Warehouse/${product.productId}`);
                if (response.ok) {
                    const data = await response.json();
                    setPriceRange({
                        minPrice: data.minPrice,
                        maxPrice: data.maxPrice,
                    });
                } else {
                    console.error('Couldn`t get a price range:', response.status);
                }
            } catch (error) {
                console.error('Couldn`t get a price range:', error);
            }
        };

        fetchPriceRange();
    }, [product.productId]);

    return (
        <div className="col mb-3">
            <div className="card h-100 product-card">
                <Link to={`/product/${product.productId}`} className="card-link">
                    <div className="card-img-top-container">
                        <img src={product.image} className="card-img-top" alt={product.productName} />
                    </div>
                    <div class="card-body">
                        <h2 class="card-title">{product.productName}</h2>
                        <h6 class="card-subtitle text-muted">Производитель: {product.manufacturer}</h6>
                    </div>
                </Link>

                <div class="card-body">
                    <h6 class="card-price">{`Цена: от ${priceRange.minPrice} до ${priceRange.maxPrice}р.`}</h6>
                    <Button
                        className={`btn rounded-pill w-100 ${isInCart ? 'btn-dark-orange' : 'btn-yellow'}`}
                        onClick={handleAddToCart}
                    >
                        {isInCart ? 'В корзине' : 'В корзину'}
                    </Button>
                </div>

                <AvailableStoresModal
                    isModalOpen={isModalOpen}
                    closeModal={() => setModalOpen(false)}
                    stores={stores}
                />

                <YandexMapModal
                    isOpen={isMapModalOpen}
                    onRequestClose={() => setMapModalOpen(false)}
                    setMapModalOpen={setMapModalOpen}
                    onAddressSaved={() => {
                        window.location.reload();
                    }}
                />

                <NavbarAuthModal show={showAuthModal} handleClose={() => setShowAuthModal(false)} />
            </div>
        </div>
    );
}

export default ProductCard;