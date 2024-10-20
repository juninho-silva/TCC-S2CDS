Feature: Campanha

Uma campanha, constitui de um titulo, descrição, um ou varios tipos sanguineos para prospecção de doadores compativeis.

@create
Scenario: Criacao de Campanha
	Given um usuario captador deseja criar uma campanha
	When passar titulo, descricao e o tipo sanguineo na request
	Then então devolver o status code 201
	And informa uma mensagem de sucesso

@read
Scenario: Visualizar Campanhas
	Given um usuario queira visualizar a campanha
	When passar o identificador no parametro da request
	Then então devolver o status code 200
	And informa os dados da campanha

@update
Scenario: Atualizacao de Campanha
	Given um usuario captador criador da campanha, desejar alterar
	When mudar os dados na request
	Then então devolver o status code 200
	And informa uma mensagem de sucesso

@delete
Scenario: Deletar Campanha
	Given um usuario captador criador da campanha, desejar deletar
	When passar o identificador no parametro da request
	Then então devolver o status code 200
	And informa uma mensagem de sucesso
