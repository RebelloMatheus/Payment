# Payment


# Transa��o com cart�o e antecipa��o de receb�veis

A Pagcerto pretende lan�ar no mercado o servi�o de antecipa��o de receb�veis, e para isso precisamos da sua ajuda. Aos lojistas/vendedores ser�o permitidos solicitar antecipa��o de receb�veis das transa��es aprovadas pela Pagcerto. Seu desafio � desenvolver um projeto que represente esse produto, sendo a implementa��o discorrida em tr�s etapas. A primeira etapa destina-se a transa��o com cart�o, a segunda tem foco no gerenciamento de antecipa��o e a terceira trata sobre os testes funcionais.

# Etapa 1: Transa��o

Transa��es s�o opera��es financeiras originadas de vendas com cart�o de cr�dito. Para cada transa��o, devem ser mantidas as seguintes informa��es:

- Identificador �nico num�rico da transa��o (NSU);
- Data em que a transa��o foi efetuada;
- Data de aprova��o (caso aprovada);
- Data de reprova��o (caso reprovada);
- Antecipado (flag que marca uma transa��o como aprovada na solicita��o de antecipa��o);
- Confirma��o da adquirente (aprovada ou recusada);
- Valor bruto da transa��o;
- Valor l�quido da transa��o (valor da transa��o subtra�do a taxa fixa);
- Taxa fixa cobrada;
- N�mero de parcelas da transa��o;
- Quatro �ltimos d�gitos do cart�o.

Para cada parcela de transa��o, devem ser mantidas as seguintes informa��es:

- Identificador �nico num�rico da parcela;
- Chave estrangeira da transa��o;
- Valor bruto da parcela;
- Valor l�quido da parcela;
- Numero da parcela;
- Valor antecipado (Esse campo s� deve ser preenchido se a transa��o for aprovada pela an�lise do financeiro, na solicita��o de antecipa��o);
- Data prevista de recebimento da parcela;
- Data em que a parcela foi repassada (Esse campo s� deve ser preenchido se a transa��o for aprovada pela an�lise do financeiro, na solicita��o de antecipa��o).

Toda transa��o aprovada deve gerar parcelas com vencimento a cada 30 dias, exemplo: Se a transa��o for de R$100,00 em 2x (duas parcelas), deve ser criado o registro de transa��o (entidade forte), conforme os crit�rios acima, e uma lista de parcelas associadas a essa transa��o (entidade fraca). Nesse exemplo, seriam geradas duas parcelas de R$49,55 cada, sendo esse valor obtido a partir do valor da transa��o, nesse caso 100 reais, subtraido a taxa fixa, 0,90, e dividido pelo n�mero de parcelas, no exemplo 2x. Sobre a data de recebimento das parcelas, ainda nesse exemplo, a primeira teria seu repasse realizado com 30 dias ap�s a data de realiza��o da venda e a segunda parcela com 60 dias a partir da mesma data de refer�ncia.

## Crit�rios de aceita��o

- Cobrar taxa fixa de 0,90 nas transa��es aprovadas;
- Na requisi��o de transa��o (efetuar pagamento), o n�mero do cart�o deve conter 16 caracteres num�ricos, sem espa�os;
- Caso o n�mero do cart�o inicie com "5999", deve ter a transa��o reprovada ao efetuar pagamento, nos demais casos v�lidos a transa��o dever� ser aprovada;
- Gerar parcelas apenas em transa��es aprovadas;
- O vencimento de cada parcela dever� ser obtido atrav�s da multiplica��o do n�mero da parcela por 30, conforme exemplificado acima;
- O valor l�quido da parcela dever� ser obtido a partir do valor bruto da transa��o subtra�do a taxa fixa, dividido pelo n�mero de parcelas (j� citado em exemplo).

## Sobre o servi�o

Construa uma API RESTful para que nossos clientes integrem seus sistemas financeiros com a sua conta da Pagcerto, oferecendo os seguintes endpoints:

- Efetuar pagamento com cart�o de cr�dito;
- Consultar uma transa��o e suas parcelas a partir do identificador da transa��o.

# Etapa 2: Antecipa��o

Solicita��es de antecipa��o s�o documentos emitidos pelo lojista/vendedor atrav�s do nosso servi�o de repasse antecipado de valores. A antecipa��o de uma transa��o tem um custo de 3.8% aplicado em cada parcela da transa��o, se aprovada pela an�lise do financeiro, sendo automaticamente debitado no seu repasse. Considerando o exemplo da transa��o citado na fase 1, se cada parcela da transa��o tem valor l�quido de 49,55, o valor antecipado da parcela seria obtido a partir desse valor l�quido, debitado a taxa de 3.8%.

Para cada solicita��o de antecipa��o, devem ser mantidas as seguintes informa��es:

- Identificador �nico da solicita��o;
- Data da solicita��o;
- Data de in�cio da an�lise;
- Data de finaliza��o da an�lise;
- Resultado da an�lise (aprovado, aprovado parcialmente ou reprovado);
- Valor solicitado na antecipa��o (soma do valor l�quido das transa��es solicitadas na antecipa��o);
- Valor antecipado (soma do valor antecipado de todas as parcelas de transa��es aprovadas na antecipa��o);
- Lista de transa��es solicitadas na antecipa��o.

## Crit�rios de aceita��o

- N�o � permitido incluir em uma nova solicita��o de antecipa��o transa��es solicitadas anteriormente;
- Para realiza��o de uma nova solicita��o de antecipa��o, � necess�rio que a solicita��o atual j� tenha sido finalizada;
- Uma transa��o com an�lise aprovada ou reprovada n�o pode ser modificada, ou seja, n�o deve permitir altera��o no status (aprovada/reprovada);
- A data de finaliza��o da an�lise deve ser preenchida quando todas as transa��es da antecipa��o forem resolvidas como aprovadas ou reprovadas;
- Aplicar taxa de 3.8% em cada parcela de transa��o antecipada, considerando o valor l�quido da parcela. Esse valor deve ser armazenado no campo "Valor antecipado" da parcela da transa��o em quest�o;
- Caso a transa��o seja aprovada na antecipa��o, ao finalizar a solicita��o, deve ter o campo "Data em que a parcela foi repassada", da entidade "Parcela", preenchida com a data atual.

O tr�mite de uma solicita��o de antecipa��o progride atrav�s das seguintes etapas:

1. Aguardando an�lise (pendente): O lojista solicitou antecipa��o, mas ainda n�o foi iniciado a an�lise pela equipe financeira da Pagcerto;
2. Em an�lise: A equipe financeira iniciou a an�lise da antecipa��o, podendo aprovar ou reprovar uma ou mais transa��es contidas na solicita��o;
3. Finalizada: Quando a an�lise da solicita��o � encerrada, a antecipa��o pode assumir um dos seguintes resultados: aprovada (todas as transa��es aprovadas), aprovada parcialmente (quando existe ao menos uma transa��o aprovada e uma transa��o reprovada na an�lise) ou reprovada (todas as transa��es reprovadas).

## Sobre o servi�o

Implemente os seguintes endpoints na API:

- Consultar transa��es dispon�veis para solicitar antecipa��o (n�o � necess�rio filtros);
- Solicitar antecipa��o a partir de uma lista de transa��es;
- Iniciar o atendimento da antecipa��o;
- Aprovar ou reprovar uma ou mais transa��es da antecipa��o (quando todas as transa��es forem finalizadas, a antecipa��o ser� finalizada);
- Consultar hist�rico de antecipa��es com filtro por status (pendente, em an�lise, finalizada).

# Etapa 3: Testes

Fa�a a implementa��o dos testes necess�rios a fim de garantir o cumprimento das regras de neg�cio descritas no desafio.

# Sobre o desenvolvimento da solu��o

Para o desenvolvimento do nosso servi�o, atente-se para algumas considera��es importantes:

1. Nossa API RESTful dever� ser desenvolvida utilizando ASP.NET Core, Entity Framework Core e SQL Server;
2. Todo o c�digo do projeto deve ser escrito em ingl�s;
3. Dever� ser utilizado o Git do pr�prio desenvolvedor para versionamento do c�digo e o reposit�rio dever� ser mantido no GitHub.

## Como testar?

1. Fazer um clone deste reposit�rio em algum lugar de sua prefer�ncia:

```
git clone https://github.com/RebelloMatheus/Payment.git
```

2. Rodas todas as APIs:
   - Ou usando o Visual Studio;
   - Ou linha de comando:

```PowerShell

dotnet build
dotnet run --project .\src\Payment.Application.WebApi\Payment.Application.WebApi.csproj

```

## Documenta��o dos Endpoint


## Ainda tem perguntas ou sugest�o de melhoria?

Sinta-se � vontade em abrir um [issue][DefeitoInnovt] ou [Pull Request][PullRequest].
