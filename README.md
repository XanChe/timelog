# Домашний проект, выполняющий несколько задач

1. Пощупать ASP.NET Core.
2. Поэксперементировать с "чистой" архитектурой, слабой связанностью.
3. Реализвать старую задумку учета времени работы над фриланс проектами.
4. Расширить её. 
---
В данный момент - это болванка, реализующая миминимальный функцианал.


## Развёртывание в docker
---
- Разимыновываем шаблон docker-compose.override.yml.tmpl путём копирования:

        cp docker-compose.override.yml.tmpl docker-compose.override.yml
    Редактируем под свои надобности. Минимум - ничего не меняем.

- В папке DeployConfigs вы полняем настройку конфигов в каждой подпапке. Для настройки читаем README.md

- Собираем контейнеры:

        sudo docker-compose build

- Поднимаем контейнеры:

        sudo docker-compose uo -d