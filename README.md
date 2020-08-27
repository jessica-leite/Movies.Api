# Movies.Api

API REST em .NET Core que realiza busca dos próximos filmes a serem lançados no cinema.

- Por se tratar de um projeto simples, está contido em apenas uma camada (apresentação). 
- Foi utilizada injeção de dependência para reduzir a dependência entre as classes.
- Uso de paralelismo para requisições assíncronas com uso da classe Task, para melhorar o tempo de resposta da aplicação (melhoria em média de 230%).
- Biblioteca de terceiros:
  - Newtonsoft - para desserialização de string json para objeto.


