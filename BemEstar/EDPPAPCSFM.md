# EXPLICAÇÃO DOS PADRÕES DE PROJETO APLICADOS - POOL DE CONEXÃO - SINGLETON - FACTORY METHOD - TEMPLATE METHOD

# PROJETO EM LINGUAGEM C# UTILIZANDO BANCO DE DADOS POSTGRESQL E FERRAMENTA VISUAL STUDIO

# Problemas: -String de conexão duplicada em vários métodos;
             -Abertura e fechamento de conexão sendo decidido por cada serviço;
             -Alta possibilidade de vazamento de conexão, estouro do pool;
             -Alta repetição de código;
             -Dificuldade em manutenção, testes e troca de banco de dados.

# Objetivo: -Centralizar e separar a abertura e fechamento de conexões com o Banco de Dados;
	        -Construir dois métodos: um que retorne dados e outro que não retorne dados;
            -Não utilizar USING.
			 
# SOLUÇÃO: -Utilizar o pool de conexões para reduzir o número de conexões abertas ao banco de dados, reutilizando conexões existentes.
           -Condensar a CRUD em apenas dois métodos, um para gerar retorno e outro que não gere retorno.


# ESCOLHA DOS PADRÕES DE PROJETO: GoF padrões criacionais + padrão comportamental 

SINGLETON (DbFactorySingleton) – Centraliza o acesso ao Banco de Dados, fornece um ponto global de acesso à fábrica, garante consistência e o pool reutilizado.
+
FACTORY METHOD (IDbConnectionFactory / NpgsqlConnectionFactory) - Cria e gerencia conexões de forma encapsulada. Define 2 métodos genéricos reutilizáveis para CRUD.
+
TEMPLATE METHOD (BaseService<T>) - Padroniza assinatura de métodos CRUD.

Sem using, a conexão precisa ser liberada manualmente para não estourar o pool do Npgsql. 
Por isso, a necessidade de centralizar a gestão do ciclo de vida da conexão utilizando a combinação de Singleton + Factory Method.


# PASSO A PASSO:

1 - Criar uma fábrica de conexões (Factory Method) que será responsável por criar e gerenciar as conexões com o banco de dados;
2 - Criar uma fábrica de conexão (PostgresConnectionFactory);
3 - Ter uma instância dessa fábrica (Singleton);
4 - Criar uma classe Base com dois métodos genéricos (ExecuteQuery e ExecuteNonquery);
5 - Refatorar DicaService para usar esses métodos eliminando repetições;
6 - DicaService herda de BaseService (Reutiliza os métodos genéricos para CRUD);
7 - DbFactorySingleton fornece a instância única da fábrica (PostgresConnectionFactory para aplicação inteira);
8 - PostgresConnectionFactory implementa IDbConnectionFactory (Para abrir e fechar conexões Npgsql).

# FLUXO DE REQUISIÇÃO:

1 - Request HTTP → enviado pelo cliente
2 - DicaController → recebe a requisição, valida dados
3 - DicaService → herda BaseService<T>, implementa CRUD, usa Factory/Singleton
4 - DbFactorySingleton → fornece instância única da fábrica de conexões
5 - NpgsqlConnectionFactory → abre conexão, executa query
6 - Banco PostgreSQL → retorna resultado
7 - Controller → retorna resposta HTTP apropriada (200, 201, 204, 404)

# ESTRUTURA DE PASTAS:

```
BemEstar.Dica/
│
├── Models/                       # Modelos de dados
│   ├── BaseModel.cs              # Classe base de todos os modelos
│   └── DicaModel.cs              # Modelo da tabela 'dica'
│
├── Interfaces/                   # Interfaces de contratos de serviços
│   └── Iservice.cs               # Interface CRUD genérica
│
├── Infrastructure/               # Camada de infraestrutura
│   ├── IDbConnectionFactory.cs   # Interface da fábrica de conexões
│   ├── NpgsqlConnectionFactory.cs# Implementação concreta da fábrica (Postgres)
│   ├── DbFactorySingleton.cs     # Singleton para instância única da fábrica
│   └── BaseDbService.cs          # Métodos genéricos de acesso ao banco (CRUD)
│
├── Services/                     # Camada de serviços
│   ├── BaseService.cs            # Classe abstrata genérica de serviços
│   └── DicaService.cs            # Serviço concreto para 'dica'
│
└── WebApi/                       # Camada de apresentação / API
    └── Controllers/
        └── DicaController.cs     # Controller REST para DicaModel
```

---


# DESCRIÇÃO DOS CÓDIGOS:

### BaseModel.cs

Classe base para todos os modelos, define o campo `Id`.

```csharp
public class BaseModel
{
    public int Id { get; set; }
}
```

### DicaModel.cs

Modelo que representa a tabela `dica` no banco de dados.

```csharp
public class DicaModel : BaseModel
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Categoria { get; set; }
}
```

---

## 🔹 Interface Iservice<T>

Define o contrato CRUD genérico para todos os serviços.

```csharp
public interface Iservice<T>
{
    void Create(T model);
    List<T> Read();
    void Update(T model);
    void Delete(int id);
    T ReadById(int id);
}
```

---

## 🔹 BaseService.cs (Classe Abstrata Genérica)

Fornece a base para qualquer serviço que acessa o banco via Factory e Singleton.

```csharp
public abstract class BaseService<T> where T : BaseModel
{
    protected readonly IDbConnectionFactory _connectionFactory;

    protected BaseService() 
    {
        _connectionFactory = DbFactorySingleton.Instance;
    }

    public abstract void Create(T model);
    public abstract void Delete(int id);
    public abstract List<T> Read();
    public abstract T ReadById(int id);
    public abstract void Update(T model);
}
```

---

## 🔹 DicaService.cs

Serviço concreto para operações CRUD na tabela `dica`.

```csharp
public class DicaService : BaseService<DicaModel>
{
    public DicaService() : base() {}

    public override void Create(DicaModel model) { /* Implementação com ExecuteNonQuery */ }
    public override void Delete(int id) { /* Implementação */ }
    public override List<DicaModel> Read() { /* Implementação com ExecuteQuery */ }
    public override DicaModel ReadById(int id) { /* Implementação com ExecuteQuerySingle */ }
    public override void Update(DicaModel model) { /* Implementação */ }
}
```

---

## 🔹 DicaController.cs

Controller REST alinhado com DI do ASP.NET Core.

```csharp
[ApiController]
[Route("api/[controller]")]
public class DicaController : ControllerBase
{
    private readonly DicaService _service;

    public DicaController(DicaService service)
    {
        _servic
```



# INJEÇÃO DE DEPENDÊNCIA:

Controller → recebe Service via DI → Service → acessa DB via Singleton da Fábrica.
Na program.cs, o `AddScoped` garante que o serviço terá uma nova instância por requisição HTTP.
Na DicaController, o serviço não é criado manualmente com new, em vez disso, ele é injetado pelo ASP.NET Core.
O framework resolve automaticamente a dependência e fornece a instância correta de DicaService.
DicaService herda de BaseService<T>: Mesmo que múltiplas instâncias de DicaService sejam criadas (uma por requisição), todas compartilham a mesma fábrica de conexões.

Vantagens: aplicação modular, testável, flexivel e escalável. Com ciclo de vida controlado: o`Scoped` garante instâncias seguras por requisição.



# VANTAGENS E GANHOS:

- Reuso de código (CRUD genérico);
- Arquitetura em camadas clara;
- Facilidade na manutenção e testes unitários;
- Pool de conexões eficiente (Npgsql);
- Fácil de escalar e adicionar novos serviços.

# CUIDADOS E DESVANTAGENS:

- Inicialmente mais complexa que CRUD direto;
- Necessário implementar SQL nos serviços concretos;
- Singleton precisa ser usado corretamente para evitar problemas de concorrência.

