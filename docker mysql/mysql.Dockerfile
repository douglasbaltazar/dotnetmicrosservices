FROM mysql:5.7

LABEL author="Douglas Baltazar"
LABEL version="1.0"

COPY *.sql /docker-entrypoint-initdb.d/