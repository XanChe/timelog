## Создание самоподписанного сертификата
> Если у вас есть сертификат - скопируйте в данную папку *.crt и *.key файлы и выполините для них только пункт 4

1. Разыменовываем localhost.conf путём копирования:

        cp localhost.conf.tmpl localhost.conf

2.  Настраиваем localhost.conf, прописываем свои DNS

3. Создаём сертификат и ключ. Шифруем
    
        openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout localhost.key -out localhost.crt -config localhost.conf -passin pass:YourStrongPassword

4. Экспортируем в pfx, задаём пароль

        openssl pkcs12 -export -out localhost.pfx -inkey localhost.key -in localhost.crt

5. Проверяем, существует ли директория /usr/local/share/ca-certificates:

        ls -l /usr/local/share/ca-certificates

    Если её ещё нет, то создаём:

        sudo mkdir /usr/local/share/ca-certificates
        
6. Копируем наш сертификат командой вида:

        sudo cp localhost.crt /usr/local/share/ca-certificates/

7. Запускаем следующую команду для обновления общесистемного списка:

        sudo update-ca-certificates