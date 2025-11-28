# Etapa 1 — Build del frontend
FROM node:20-slim AS builder

WORKDIR /app

COPY package*.json ./

# Instalar dependencias
RUN npm install

# Copiar código
COPY . .

RUN chmod +x node_modules/.bin/vite

# Build del frontend
RUN npm run build

# Etapa 2 — Servir con NGINX
FROM nginx:1.25

WORKDIR /usr/share/nginx/html

# Copia el build al servidor
COPY --from=builder /app/dist .

EXPOSE 3000
