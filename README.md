<h1 align="center"> Teste prático API de gerenciamento de caminhões </h1>
1. URL: https://localhost:44378/swagger/index.html

2. Para Rodar o projeto é de suma importancia alterar a ConnectionString no arquivo appsettings.json dentro do projeto Truck.Management.Test.API, apontando para o banco local onde o teste será feito.

3. Após isso é só rodar o projeto

4. Foram feito os testes unitarios de toda a camada de aplicação e dominio onde estão a regra de negócio.
5. Exemplo de Post pelo swagger para criar um caminhão 
- {
  "cor": "branco",
  "modelo": "FM",
  "anoModelo": 2023,
  "digitalPanel": true
}

- {
  "cor": "amarelo",
  "modelo": "FH",
  "anoModelo": 2022
}

6. Exemplo de Update PUT feito pelo swagger
- {
  "id": 1008,
  "cor": "cinza escuro",
  "modelo": "FH",
  "anoModelo": 2022,
  "digitalPanel": true
}
