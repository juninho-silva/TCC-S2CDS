Feature: Authentication

@geracao_token
Scenario: Gerar token com sucesso
	Given um usuario valido
	When passar as credenciais via POST ao '/api/v1/authentication'
	Then deve retornar um status code 200
	And retornar o token
