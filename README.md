# Papelaria-API
Esse projeto implementa uma API que permite o controle do estoque de produtos de uma Papelaria.
Essa api disponibiliza endpoints para: CRUD de produtos, entrada e saíde de estoque, consulta de histórico de movimentações e alertas de estoque mínimo.

# Padrões de Projeto
- Linguagem C#
- Solid: Em toda estrutura são aplicados princípios de SOLID, como seperação de responsabilidades, inversão de dependência, Single Responsability Principle, e etc.
- Estrutura de Camadas: Esse projeto está divido entre quatro camadas de API, Domain, Crosscutting e Infra.
- Patterns: Repository, Factory, Builders e Mediator
- Dependency Injection
- Unit Tests
- DTOs

# Regras De Negócio
- Ao registrar uma entrada de estoque, deve-se atualizar a quantidade e armazenar um log (em banco) com data e usuário responsável
- Ao registrar uma saída de estoque, deve-se validar se há quantidade suficiente, atualizar a quantidade e armazenar um log em banco.
- Se a quantidade atual for menor que a quantidade mínima, o produto deve ser marcado como "estoque crítico"
- A busca de produtos deve permitir filtros por nome, categoria ou status do estoque (ok/critico)
