package com.example;

import io.cucumber.java.Before;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.When;
import io.cucumber.java.en.Then;
import io.cucumber.datatable.DataTable;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

public class OrderPricingStepDefs {

    private OrderService orderService;
    private OrderSummary orderSummary;
    private List<Promotion> promotions;
    private List<Map<String, String>> orderItemsData;

    @Before
    public void setUp() {
        promotions = new ArrayList<>();
        orderService = new OrderService();
    }

    @Given("no promotions are applied")
    public void no_promotions_are_applied() {
        // State is reset in @Before hook
    }

    @Given("the threshold discount promotion is configured:")
    public void the_threshold_discount_promotion_is_configured(DataTable dataTable) {
        Map<String, String> config = dataTable.asMaps().get(0);
        int threshold = Integer.parseInt(config.get("threshold"));
        int discount = Integer.parseInt(config.get("discount"));
        promotions.add(new Promotion("ThresholdDiscount", threshold, discount, null));
    }

    @Given("the buy one get one promotion for cosmetics is active")
    public void the_buy_one_get_one_promotion_for_cosmetics_is_active() {
        promotions.add(new Promotion("BuyOneGetOne", 0, 0, "cosmetics"));
    }

    @Given("the Double Eleven promotion is active")
    public void the_double_eleven_promotion_is_active() {
        promotions.add(new Promotion("DoubleElevenDiscount", 0, 0, null));
    }

    @When("a customer places an order with:")
    public void a_customer_places_an_order_with(DataTable dataTable) {
        orderItemsData = dataTable.asMaps(String.class, String.class);
        orderSummary = orderService.calculateOrderSummary(orderItemsData, promotions);
    }

    @Then("the order summary should be:")
    public void the_order_summary_should_be(DataTable dataTable) {
        Map<String, String> summary = dataTable.asMaps().get(0);
        if (summary.containsKey("totalAmount")) {
            int expectedTotalAmount = Integer.parseInt(summary.get("totalAmount"));
            assertEquals(expectedTotalAmount, orderSummary.getTotalAmount());
        }
        if (summary.containsKey("originalAmount")) {
            int expectedOriginalAmount = Integer.parseInt(summary.get("originalAmount"));
            assertEquals(expectedOriginalAmount, orderSummary.getOriginalAmount());
        }
        if (summary.containsKey("discount")) {
            int expectedDiscount = Integer.parseInt(summary.get("discount"));
            assertEquals(expectedDiscount, orderSummary.getDiscount());
        }
    }

    @Then("the customer should receive:")
    public void the_customer_should_receive(DataTable dataTable) {
        List<Map<String, String>> expectedItems = dataTable.asMaps();
        List<OrderItem> actualItems = orderSummary.getShippedItems();

        assertEquals(expectedItems.size(), actualItems.size());

        for (Map<String, String> expected : expectedItems) {
            boolean found = actualItems.stream().anyMatch(actual ->
                    expected.get("productName").equals(actual.getProduct().getName()) &&
                    Integer.parseInt(expected.get("quantity")) == actual.getQuantity());
            if (!found) {
                // A more detailed assertion message would be helpful here
                throw new AssertionError("Expected item not found or quantity mismatch: " + expected);
            }
        }
    }
} 