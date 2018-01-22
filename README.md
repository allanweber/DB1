# Teste para DB1 Global Software

# Para Rodar

**Para o os projetos que estão no Visual Studio, que são:**
 - Exercicie1
 - Todos dentro do diretório RHApp
 Restaurar os pacotes do NuGet
 Dentro do diretório DB1Tasks existe uma solution para abrir esses projetos.

**Para o projeto com interface web que está no diretório Exercicie2:**

 - Rodar o mando ***npm install*** para instalar as dependências 
 - Rodar o comando ***ng serve*** para subir a aplicação que ficará acessível no endereço **http://localhost:4200**

# Sobre as escolhas e arquitetura
**Exercício 1**
Foi feito uma console application, que basta ser executada para obter os resultados, as taks foram resolvidas da maneira mais simples possível para não perder muito do curto tempo para execução do teste

**Exercício 2**
Optei por utilizar angular-cli para ter um start mais rápido do projeto é começar logo a codificar.
Foi utilizado angular 5 e nenhuma pacote adicionar foi necessário, apenas o bootstrap afim de dar um melhor acabamento na interface.
A aplicação foi separada em dois grandes componentes, navbar e users.
A navbar apenas para navegabilidade da aplicação, usando rotas simples, sem child routes, nem lazyloading de modules.
Os componentes de users são basicamente uma listagem e uma detalhamento, sendo que esse último faz uso de outros dois sub componentes, uma para a listagem dos repositórios injetando o serviço nele mesmo pois não seria usado em outro local, e outro abstraindo a interface de detalhes do usuário evitando muita repetição de código.

**Exercício 3**
Foi usado DDD onde 
RH.Domain, como o nome diz possuí o domínio da aplicação, divididos em:
 - CommandHandlers: Usando o princípio de CQRS (Command-Query Responsibility), que separa a responsabilidade de quem alterada dados. Aqui ainda existem os comandos que abstraem as informações que serão enviadas pelas APIs e posteriormente mapeadas via AutoMapper para as entidades afim de não expor nossas entidades. Ainda 
 - Core: Algumas responsabilidades e objetos básicos que normalmente ficariam em um  framework para todas as aplicações. As uncionalidades implementadas aqui são a Entidade Base (BaseEntity), interface básica do repositório e as interfaces e classes de ICommand para sucesso e falha no uso de CQRS, IDto e IService foram pensados numa futura extensão da aplicação e aplicar tratamentos e comportamentos nesses tipos de objetos.
 - Dtos: Objetos de Data tranfer para serem respondidos pelas APIS.
 - Entities: As entidades que mantêm os dados da aplicação.
 - Repositories: Assinaturas dos repositórios de manipulação de dados.
 - Services: Serviços com lógica mais complexa da aplicação.

RH.Infrastructure, além de implementar as classes concretas do domínio ainda:
 - Mappers: Configurações de AutoMapper entre Entidades e Dtos e vice-versa.
 - Repositories: Além das classes concretas, possui a classe básica de Repository e o DbContext das aplicação, que normalmente eu colocaria no framework.
 - Repositories.Mappers: Mapeamentos das entidades com banco relacional.
 - Aqui foi implementada a lógica via Service para triagem dos candidatos mediate pontuação de conhecimento em determinadas tecnologias.

RH.Api, Api propriamente, responde as informações do domínio e infraestrutura, ela está separada em:
 - Core: Possui uma controller básica (BaseCrudController) que abstrai os verbos HTTP para Get (nesse caso GetAll), POST, PUT e DELETE. Mediante uma assinatura com Generics, usando as interfaces citadas anteriormente toda a lógica básica de crud via repositório e CQRS fica abstraída e sem repetição, não ferindo os princípios de DRY (Don't repeat yourself)
 - Filters: Aqui existe um FilterAttribute usado para o tratamento do retorno dos métodos CQRS e caso um objeto do tipo FailureResult esteja sendo retornado, o status code da aplicação será sempre 400.
 - Middlewares: aqui faço o "tratamento" de erros não tratados, levando para a saída da API um objeto Json mais fácil de ser lido e compreendidom sempre que qualquer exceção não tratada ocorrer será retornado um objeto contendo as propriedades: ExceptionMessage, ExceptionStackTrace e ExceptionDetail.
 - Startup.cs: Aqui são feitas as configurações básicas da aplicação, como os serviços que ela usará, configuração de Filters e Middlewares além da injeção de dependência, que usei a nativa do .netcore. Aqui também são feitas duas outras configurações importantes: O Swagger para exibir as APIS e o uso de uma banco em memória para quando o ambiente for teste, assim são "sujando" nosso banco de dados.
 
 RH.Tests, os testes da aplicação, usando o ambiente IntegrationTests que será em memória. Foi usado Xunit para os testes
 *Os comandos ServiceCollectionExtensions.UseStaticRegistration = false em todos os testes são devido ao AutoMapper estar sendo registrado mais de uma vez, o que causa erro.*
 Todos os testes foram feitos de maneira integrada, consumindo as APIS diretamente.

# Tecnologias e Pacotes Usados
 1. Xunit para os teste
 2. EF Core para acesso a dados
 3. AutoMapper para mapeamento de objetos
 4. MediatR para o CQRS 
 5. MySql como banco de dados
 6. Swagger para documentar as APIS
 7. .NET Core 2.0 para os projetos backend
 8. Angular 5.2.0 e angular-cli 1.6.5 para o projeto de front-end.

# O que faltou e melhorias
 - Não tive tempo para criar a interface da aplicação de RH, assim como criar mais testes e performar o serviço de triagem.
 - Faltou Logger assincrono 
 - UnitOfWork.
 - Abstrair o serviço de CQRS para no futuro poder enfileirar algumas chamadas talvez com rabbit.
 - Um serviço de notification handler ligado os commands do CQRS para ligar eventos quando os dados da aplicação fossem alterados.
 - Fazer uso dos cancellationToken dos CommandHandler para cancelamento entre threads.
 - etc.

# PS.: Criei um banco no meu provedor de serviços.
Assim vocês não precisam criar o banco para testar o consumo das apis, **peço que me avisem assim que terminarem para eu remover esse banco**
**Os dados de acesso da banco estão no appsettings.json**

