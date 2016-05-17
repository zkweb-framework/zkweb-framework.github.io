ZKWeb使用了NHibernate来管理数据库和查询数据。<br/>
目前支持的数据库服务器有PostgreSQL, SQLite, MSSQL, MySQL。<br/>
使用NHibernate的理由有<br/>

- 不保存数据库状态和更新历史，不会像EF一样容易出现metadata相关的错误
- 可以实现自动更新数据库，添加字段后不需要任何额外操作就可以应用到数据表
- 更好的支持MySQL等非微软的数据库，不会像EF只对MSSQL支持好。

ZKWeb同时使用了FluentNHibernate来定义数据结构，避免使用繁杂的xml。<br/>

以下内容待编写<br/>

### 添加数据表

### 升级数据表

### 增删查改

### 添加数据事件
