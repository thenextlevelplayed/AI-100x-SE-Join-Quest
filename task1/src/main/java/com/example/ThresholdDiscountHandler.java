package com.example;

public class ThresholdDiscountHandler implements PromotionHandler {
    @Override
    public OrderSummary apply(OrderSummary summary, Promotion promotion) {
        if (!"ThresholdDiscount".equals(promotion.getType())) {
            return summary;
        }

        if (summary.getOriginalAmount() >= promotion.getThreshold()) {
            int newDiscount = summary.getDiscount() + promotion.getDiscount();
            int newTotalAmount = summary.getOriginalAmount() - newDiscount;
            return new OrderSummary(
                    summary.getShippedItems(),
                    summary.getOriginalAmount(),
                    newDiscount,
                    newTotalAmount
            );
        }

        return summary;
    }
} 