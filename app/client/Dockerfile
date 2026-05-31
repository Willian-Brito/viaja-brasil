# =========================
# Build
# =========================
FROM node:22-alpine AS build

WORKDIR /app

COPY app/client/package*.json ./

RUN npm install

COPY app/client .

RUN npm run build

# =========================
# Runtime
# =========================
FROM nginx:alpine

COPY --from=build /app/dist /usr/share/nginx/html

# Expor a porta 8081
EXPOSE 8081

CMD ["nginx", "-g", "daemon off;"]