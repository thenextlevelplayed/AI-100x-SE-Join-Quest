package com.example;

import java.util.ArrayList;
import java.util.List;

public class BuyOneGetOneHandler implements PromotionHandler {
    @Override
    public OrderSummary apply(OrderSummary summary, Promotion promotion) {
        if (!"BuyOneGetOne".equals(promotion.getType())) {
            return summary;
        }

        List<OrderItem> newShippedItems = new ArrayList<>();
        for (OrderItem item : summary.getShippedItems()) {
            if (promotion.getTargetCategory().equals(item.getProduct().getCategory())) {
                newShippedItems.add(new OrderItem(item.getProduct(), item.getQuantity() + 1));
            } else {
                newShippedItems.add(item);
            }
        }

        // BOGO only affects the items shipped, not the amounts paid.
        return new OrderSummary(
                newShippedItems,
                summary.getOriginalAmount(),
                summary.getDiscount(),
                summary.getTotalAmount()
        );
    }
} 