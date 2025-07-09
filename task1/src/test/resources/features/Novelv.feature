@November_discount_event
Feature: November Double Eleven Discount
  As a shopper
  I want my order to apply the Double Eleven discount rules
  So that I can save money on qualifying purchases

  Background:
    Given the Double Eleven promotion is active

  Scenario Outline: Applies 20% discount for same product items
    When a customer places an order with:
      | productName | quantity | unitPrice |
      | <productName> | <quantity> | <unitPrice> |
    Then the order summary should be:
      | originalAmount | discount | totalAmount |
      | <originalAmount> | <discount> | <totalAmount> |
    And the customer should receive:
      | productName | quantity |
      | <productName> | <quantity> |

    Examples:
      | productName | quantity | unitPrice | originalAmount | discount | totalAmount |
      | socks       | 12       | 100       | 1200           | 200      | 1000        |
      | socks       | 27       | 100       | 2700           | 400      | 2300        |

  Scenario: No discount for 10 different product items
    When a customer places an order with:
      | productName | quantity | unitPrice |
      | A           | 1        | 100       |
      | B           | 1        | 100       |
      | C           | 1        | 100       |
      | D           | 1        | 100       |
      | E           | 1        | 100       |
      | F           | 1        | 100       |
      | G           | 1        | 100       |
      | H           | 1        | 100       |
      | I           | 1        | 100       |
      | J           | 1        | 100       |
    Then the order summary should be:
      | originalAmount | discount | totalAmount |
      | 1000           | 0        | 1000        |
    And the customer should receive:
      | productName | quantity |
      | A           | 1        |
      | B           | 1        |
      | C           | 1        |
      | D           | 1        |
      | E           | 1        |
      | F           | 1        |
      | G           | 1        |
      | H           | 1        |
      | I           | 1        |
      | J           | 1        |