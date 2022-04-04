
-  Настраиваем localhost.conf, прописываем свои DNS

- Создаём сертификат и ключ. Шифруем
    
        openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout localhost.key -out localhost.crt -config localhost.conf -passin pass:YourStrongPassword

- Экспортируем в pfx, задаём пароль

        openssl pkcs12 -export -out localhost.pfx -inkey localhost.key -in localhost.crt

- Проверяем, существует ли директория /usr/local/share/ca-certificates:

        ls -l /usr/local/share/ca-certificates

    Если её ещё нет, то создаём:

        sudo mkdir /usr/local/share/ca-certificates
        
- Копируем наш сертификат командой вида:

        sudo cp localhost.crt /usr/local/share/ca-certificates/

- Запускаем следующую команду для обновления общесистемного списка:

        sudo update-ca-certificates