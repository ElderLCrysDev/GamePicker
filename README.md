
//------------------------------// PT - BR //------------------------------//
"openapi": "3.0.1"
"titulo": "GamePicker.Api"
"versao": "1.0"
"criador": Luan Crystian Fonseca de Andrade
"data de criacao: "17/09/2025"

Pré-requisitos:
.NET 7 SDK instalado
Visual Studio 2022 (ou superior) com suporte para ASP.NET Core
Conexão com a internet para acessar a API externa FreeToGame

Descricao:
API para recomendacao de jogos gratuitos com base em filtros como categoria, plataforma e memoria RAM disponivel. Os dados sao obtidos atraves da API externa FreeToGame.

Como rodar a API:
1- Clone o repositório: git clone https://github.com/ElderLCrysDev/GamePicker.git;
2- Abra o arquivo GamePicker.sln pelo Visual Studio;
3- Restaure os pacotes NuGet: Ferramentas > Gerenciador de Pacotes do NuGet > Gerenciar Pacotes do NuGet para a solucao e restaure;
4- Rode o projeto GamePicker.Api como projeto de inicialização;
5- Acesse o Swagger para documentação e teste: https://localhost:5001/swagger;
6- Tambem ha o teste via xUnit + Moq em GamePicker.Tests: Teste > Executar todos os testes 

Stacks:
-ASP.NET Core 6
-Entity Framework Core
-SQLite (In-Memory para testes)
-Swashbuckle (Swagger)
-xUnit + Moq

Endpoints:
(GET /recommendations)
- Recomenda um jogo aleatorio com base nos filtros fornecidos.

Parametros da Query (RecommendationRequest):
Nome:category Tipo:string[]	Obrigatorio: Sim	
Nome:platform Tipo:int Obrigatorio: Sim	Values:0 = PC, 1 = Browser, 2 = Both
Nome:availableRam Tipo:int Obrigatorio: Nao	

Ex:
GET /recommendations?category=Action&category=Shooter&platform=0&availableRam=8192

Resposta
[
  {
    "title": "Call of Duty: Warzone",
    "game_url": "https://www.freetogame.com/game/call-of-duty-warzone"
  }
]

//------------------------------//

(GET /recommendations/all)
-Retorna todos os jogos recomendados salvos no banco de dados.

Ex:
GET /recommendations/all

Resposta
[
  {
    "title": "Call of Duty: Warzone",
    "category": "Shooter"
  },
  {
    "title": "Warframe",
    "category": "Shooter"
  },
  {
    "title": "Apex Legends",
    "category": "Shooter"
  }
]


//------------------------------// EN - US //------------------------------//
"openapi": "3.0.1"
"title": "GamePicker.Api"
"version": "1.0"
"creator": Luan Crystian Fonseca de Andrade
"creation date: "09/17/2025"

Description:
API for recommending free games based on filters such as category, platform, and available RAM. Data is obtained through the external FreeToGame API.

How to run the API:
1- Clone the repository: git clone https://github.com/ElderLCrysDev/GamePicker.git;
2- Open the file GamePicker.sln in Visual Studio;
3- Restore the NuGet Packages: Tools > NuGet Package Manager > Manage NuGet Packages for Solution and restore it;
4- Run the project GamePicker.Api as startup project;
5- Acess the Swagger for documentation and tests: https://localhost:5001/swagger;
6- There is also tests using xUnit + Moq in GamePicker.Tests: Tests > Execute all the tests

Stacks:
-ASP.NET Core 6
-Entity Framework Core
-SQLite (In-Memory for tests)
-Swashbuckle (Swagger)
-xUnit + Moq

Endpoints:
(GET /recommendations)
- Recommends a random game based on the filters provided.

Query Parameters (RecommendationRequest):
Name:category Type:string[]	required: Yes	
Name:platform Type:int required: Yes	Values:0 = PC, 1 = Browser, 2 = Both
Name:availableRam Type:int required: No	

Ex:
GET /recommendations?category=Action&category=Shooter&platform=0&availableRam=8192

Return
[
  {
    "title": "Call of Duty: Warzone",
    "game_url": "https://www.freetogame.com/game/call-of-duty-warzone"
  }
]

//------------------------------//

(GET /recommendations/all)
-Return all the previous recomended games stored in the database.

Ex:
GET /recommendations/all

Return
[
  {
    "title": "Call of Duty: Warzone",
    "category": "Shooter"
  },
  {
    "title": "Warframe",
    "category": "Shooter"
  },
  {
    "title": "Apex Legends",
    "category": "Shooter"
  }
]

