FROM node:latest

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm install react-icons animate.css @fortawesome/react-fontawesome @fortawesome/free-solid-svg-icons @pbe/react-yandex-maps

CMD ["npm", "start"]