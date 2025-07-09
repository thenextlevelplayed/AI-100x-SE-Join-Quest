package com.example;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

public class OrderService {
    private final Map<String, PromotionHandler> promotionHandlers;

    public OrderService() {
        this.promotionHandlers = Map.of(
                "ThresholdDiscount", new ThresholdDiscountHandler(),
                "BuyOneGetOne", new BuyOneGetOneHandler(),
                "DoubleElevenDiscount", new DoubleElevenDiscountHandler()
        );
    }

    public OrderSummary calculateOrderSummary(List<Map<String, String>> orderItemsData, List<Promotion> promotions) {
        List<OrderItem> orderItems = orderItemsData.stream()
                .map(itemData -> {
                    String productName = itemData.get("productName");
                    int quantity = Integer.parseInt(itemData.get("quantity"));
                    int unitPrice = Integer.parseInt(itemData.get("unitPrice"));
                    String category = itemData.getOrDefault("category", null);
                    Product product = new Product(productName, unitPrice, category);
                    return new OrderItem(product, quantity);
                })
                .collect(Collectors.toList());

        int originalAmount = orderItems.stream().mapToInt(OrderItem::getSubtotal).sum();
        OrderSummary summary = new OrderSummary(orderItems, originalAmount, 0, originalAmount);

        if (promotions != null) {
            // Define order of promotion application
            List<String> promotionOrder = List.of("BuyOneGetOne", "ThresholdDiscount", "DoubleElevenDiscount");

            for (String promotionType : promotionOrder) {
                for (Promotion promotion : promotions) {
                    if (promotion.getType().equals(promotionType)) {
                        PromotionHandler handler = promotionHandlers.get(promotion.getType());
                        if (handler != null) {
                            summary = handler.apply(summary, promotion);
                        }
                    }
                }
            }
        }

        return summary;
    }
} 