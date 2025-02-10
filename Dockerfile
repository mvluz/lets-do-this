FROM postgres:17

# Instalar gettext para usar o comando envsubst
RUN apt-get update && apt-get install -y gettext

# Copiar o script de inicialização para o diretório correto
COPY init.sh /docker-entrypoint-initdb.d/init.sh
COPY init.sql.template /docker-entrypoint-initdb.d/init.sql.template
