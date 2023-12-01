# Ganhos de Capital

## Descrição

O objetivo deste programa é calcular o imposto a ser pago sobre lucros ou prejuízos de operações no mercado financeiro de ações.


## Solução técnica

O tipo de projeto escolhido para criação deste programa foi o Console Application, utilizando a plataforma .NET Core 8 em sistema operacional Windows.

Um arquivo com operações de exemplo foi incluído no diretório `Files` na raiz do projeto `CapitalGainsProgram`, o qual pode ser alterado para incluir novas operações a serem testadas.

Nenhuma biblioteca externa foi utilizada, apenas namespaces nativos do .NET Core.

Estrutura do projeto principal:

Classes:
* **_Processor.cs_: classe com o serviço principal de processamento de operações e cálculo de impostos;**
* _Functions.cs_: funções utilizadas para cálculos e validações;
* _Program.cs_: classe principal executada para inicialização do programa;

Diretórios:
* _Files_: diretório com arquivo de texto usado para armazenar as operações em formato json a serem testadas na solução;
* _Models_: diretório com os modelos dos contratos utilizadas como entrada e saída de dados;

<br>

## Como executar este programa

Você pode executar este programa utilizando o Visual Studio Code (https://code.visualstudio.com/), executando os comandos abaixo para cada projeto:

* Projeto principal

    Utilizando o terminal do VS Code, acesse o seguinte diretório: `.\CapitalGainsProgram\CapitalGainsProgram` e execute os comandos abaixo:
    
    <br>

    _`dotnet build`_

    _`dotnet run ".\Files\Operations.txt"`_

    <br>

* Unit Tests

    Acesse o seguinte diretório `.\CapitalGainsProgram\CapitalGainsTests` e execute o comando abaixo:    

    <br>

    _`dotnet build`_
    
    _`dotnet test`_

<br>

## Como gerar uma imagem conteinerizada deste programa

Utilizando o terminal do Visual Studio Code, dentro diretório principal do projeto `.\CapitalGainsProgram\CapitalGainsProgram`, execute os seguintes comandos:

_`dotnet build`_

_`docker build -t capital-gains -f Dockerfile .`_

<br>

Para executar esta imagem utilizando o arquivo `Operations.txt` que foi copiado durante a build da imagem, digite o seguinte comando no terminal:

_`docker run -ti --rm capital-gains './Operations.txt'`_


<br>

## Como compilar este programa

Utilizando o terminal no Visual Studio Code, dentro do diretório principal do projeto `.\CapitalGainsProgram\CapitalGainsProgram` execute o comando abaixo:

_`dotnet publish -c Release`_

Em sistema operacional Windows o executável do programa estará no seguinte diretório:

`.\CapitalGainsProgram\CapitalGainsProgram\bin\Release\net8.0\publish\`

Uma vez dentro desse diretório, basta digitar o seguinte comando para executar a aplicação usando o arquivo de texto sugerido como input das operações:

`.\CapitalGainsProgram.exe "..\..\..\..\Files\Operations.txt"`

