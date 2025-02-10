FROM cirrusci/flutter:stable AS build

# Instala o Git para permitir o uso de git clone
RUN apt-get update && apt-get install -y git

# Cria um usuário não root para executar os comandos do Flutter
RUN useradd -m flutteruser

# Altera para o usuário root para poder instalar o Flutter sem restrições
USER root

# Remove o diretório /sdks/flutter se ele existir
RUN rm -rf /sdks/flutter

# Baixa e instala o Flutter no canal estável
RUN git clone https://github.com/flutter/flutter.git /sdks/flutter -b stable

# Configura o PATH para o Flutter
ENV PATH="/sdks/flutter/bin:${PATH}"

# Marca o diretório do Flutter como seguro para o Git
RUN git config --global --add safe.directory /sdks/flutter

# Altera a propriedade do diretório /sdks/flutter para o usuário flutteruser
RUN chown -R flutteruser: /sdks/flutter

# Volta para o usuário não root
USER flutteruser

WORKDIR /app

# Copia o pubspec.yaml e pubspec.lock antes de rodar o flutter pub get
COPY pubspec.yaml pubspec.lock ./

# Copia o restante dos arquivos para dentro do container
COPY . .

# Altera para o usuário root para poder instalar dependências
USER root

# Instalar dependências
RUN flutter pub get

# Garante que o diretório lib existe (evita erro caso esteja faltando)
RUN mkdir -p lib

# Compila o Flutter Web
RUN flutter build web

# Usa um servidor web leve para rodar a aplicação
FROM nginx:alpine

WORKDIR /usr/share/nginx/html

# Copia os arquivos do build para o servidor
COPY --from=build /app/build/web .

# Expor a porta padrão do Nginx
EXPOSE 80

# Comando para rodar o Nginx
CMD ["nginx", "-g", "daemon off;"]
