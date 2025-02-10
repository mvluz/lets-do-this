#!/bin/bash

# Copiar o template para um diretório temporário com permissões adequadas
cp /docker-entrypoint-initdb.d/init.sql.template /tmp/init.sql.template

# Gerar o init.sql com as variáveis de ambiente
envsubst < /tmp/init.sql.template > /tmp/init.sql

# Executar o script de inicialização
psql -U "$POSTGRES_USER" -d "$POSTGRES_DB" -f /tmp/init.sql
