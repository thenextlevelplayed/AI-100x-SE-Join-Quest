package com.example;

public class DoubleElevenDiscountHandler implements PromotionHandler {
    private static final int DISCOUNT_THRESHOLD_QUANTITY = 6;
    private static final int DISCOUNT_AMOUNT_PER_THRESHOLD = 100;

    @Override
    public OrderSummary apply(OrderSummary summary, Promotion promotion) {
        if (!"DoubleElevenDiscount".equals(promotion.getType())) {
            return summary;
        }

        int totalDiscount = summary.getDiscount();
        for (OrderItem item : summary.getShippedItems()) {
            int quantity = item.getQuantity();
            if (quantity >= DISCOUNT_THRESHOLD_QUANTITY) {
                int discountCount = quantity / DISCOUNT_THRESHOLD_QUANTITY;
                totalDiscount += discountCount * DISCOUNT_AMOUNT_PER_THRESHOLD;
            }
        }

        int newTotalAmount = summary.getOriginalAmount() - totalDiscount;

        return new OrderSummary(
                summary.getShippedItems(),
                summary.getOriginalAmount(),
                totalDiscount,
                newTotalAmount
        );
    }
} 