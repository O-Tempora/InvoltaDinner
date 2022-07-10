Порядок развертывания:

1. Создать новую директорию
2. Положить в нее проекты lunch-backend, lunch-frontend и файл docker-compose.yml
    Структура проекта:
    - <Созданная_директория>
        |__ lunch_backend
        |__ lunch-frontend
        |__ docker-compose.yml
3. В файле lunch_backend/Dinner/SenderCredentials.json в полях Email и Password указать почту, с которой будут отсылаться пароли для восстановления, и ее пароль 
4. Убедиться, что в файлах lunch-backend/Dinner/appsettings.json и lunch-backend/DAL/Data/dinnerContext в строках подключения указано "server=database"
5. Убедиться, что в файле lunch-backend/Dinner/Properties/launchSettings.json в "profiles", "applicationUrl" первым параметром указано https://database
6. Открыть терминал в созданной директории и прописать команды "docker-compose build" и "docker-compose up -d"