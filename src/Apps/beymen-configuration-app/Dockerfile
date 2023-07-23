# Üretim aşaması için hafif bir Nginx sunucusu kullanıyoruz
FROM nginx:alpine

# Nginx konfigürasyonunu yüklüyoruz (React uygulamasının çalışacağı portu belirtiyoruz)
COPY nginx.conf /etc/nginx/conf.d/default.conf

# React uygulamasını oluşturduğumuz "build" dizinini kopyalıyoruz
COPY build /usr/share/nginx/html

# Nginx'i başlatıyoruz
CMD ["nginx", "-g", "daemon off;"]
