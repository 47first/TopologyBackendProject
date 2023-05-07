# Схема БД

  <img src="https://sun9-60.userapi.com/impg/uTHpAZNP6LcpxvKdhQ_qpRwTjLQPWz3kDSmD-w/hhkrvQDMkIA.jpg?size=219x117&quality=96&sign=a5615f9bb4d751b654efdeecda9fbae4&type=album" />

# Проекты

## TopologyProject ( Логика связанная с бэкендом )
- **/Controllers**
  - **HomeController.cs**
    - *Index() - Отправляет стандартную страницу*
  - **/Features**
    - **FeaturesExportController.cs** - Контроллер управляющий экспортом
      -  *Export() - Отправляет пользователю Json с топологией*
    - **FeaturesGetterController.cs** - Контроллер отправляющий данные о топологии
      -  *GetAllFeatures() - Отправляет все компоненты топологии в формате Json*
      -  *GetFeaturesByGroup(int id) - Отправляет компоненты топологии с группой {id} в формате Json*
    - **FeaturesImportController.cs** - Контроллер управляющий импортом
      - *SetDefault() - Импортирует стандартную топологию*
      - *SendImportForm() - Отправляет форму для импорта*
      - *ImportFromFormFile() - Импортирует топологию, полученную из формы*
    - **FeaturesJsonController.cs** - *Абстрактный класс, для импорта и экспорта топологии*
- **/Data Base**
  -  **FeatureDbModel.cs** - *Модель объекта таблицы в БД*
  - **FeaturesDbContext.cs** - *Контекст базы данных*
    - *Clear()* - *Очистка всех данных из БД*
- **/Feature**
  - **/Geometry**
    - **/JsonConverter** - *Кастомные Json конвертеры*
      - **GeometryJsonConverter.cs** - *partial класс, который управляет конвертацией геометрий в Json* 
        - *CanConvert(Type typeToConvert)* - *Может ли пройти конвертация типа*
      - **GeometryJsonRead.cs** - *Часть конвертера, управляющая чтением данных*
        - *Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)*
      - **GeometryJsonWrite.cs** - *Часть конвертера, управляющая записью данных*
        - *Write(Utf8JsonWriter writer, Geometry value, JsonSerializerOptions options)*
    - **Coordinate.cs** - *Структура хранящая XY координату*
    - **Geometry.cs** - *Абстрактный класс для геометрий*
    - **LineString.cs** - *Геометрия представляющая линию*
    - **Point.cs** - *Геометрия представляющая точку*
  - **Feature.cs** - *Тип, представляющий "достопримечательность" в топологии*
  - **FeatureCollection.cs** - *Коллекция "достопримечательностей"*
- **/Server Data** - *Ресурсы, которые доступны только для сервера*
- **/Topology**
  - **GroupDistributor.cs** - *Распределяет элементы топологии на группы*
    - *DistributeByGroups()*
  - **GroupLevel.cs** - *Структура хранящая информацию о группе*
  - **TopologyComponent.cs** - *Компонент топологии*
    - *AddFeatureConnection()* - *Создает связь между другими компонентами топологии*
  - **TopologyCreator.cs** - *Создает топологию из набора Feature (статический)*
    - *CreateTopology()*
- **/Views** - *Представления (.cshtml)*
  - **/FeaturesImport**
    - **ImportForm.cshtml** - *Форма для отправки файла на сервер*

## TopologyProjectTests ( Логика связанная с тестами )

 - **GeometryJsonTest.cs** - *Тесты связанные с полиморфным JsonConverter'ом для Geometry*
   - *IsInitialAndSerializedValuesEquals()* - *Равны ли сериализованная версия объекта и изначальная*
   - *ReadWithoutExceptions()* - *Проходит ли чтение без исключений*
   - *WriteWithoutExceptions()* - *Проходит ли запись без исключений*
