# Используем за основу образ Node.js с Alpine Linux
FROM node:latest AS build

# Устанавливаем рабочую директорию внутри контейнера
WORKDIR /app

# Копируем файлы package.json и package-lock.json для установки зависимостей
COPY package*.json ./

# Устанавливаем зависимости
RUN npm install

# Копируем все файлы вашего приложения внутрь контейнера
COPY . .

# Собираем приложение
RUN npm run build

# Открываем порт 4200
EXPOSE 4200

# Команда для запуска Angular CLI внутри контейнера
CMD ["npm", "start", "--", "--host", "0.0.0.0", "--port", "4200"]