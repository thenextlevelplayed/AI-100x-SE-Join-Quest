package com.example;

import java.util.List;

public class OrderSummary {
    private final List<OrderItem> shippedItems;
    private final int originalAmount;
    private final int discount;
    private final int totalAmount;

    public OrderSummary(List<OrderItem> shippedItems, int originalAmount, int discount, int totalAmount) {
        this.shippedItems = shippedItems;
        this.originalAmount = originalAmount;
        this.discount = discount;
        this.totalAmount = totalAmount;
    }

    public List<OrderItem> getShippedItems() {
        return shippedItems;
    }

    public int getOriginalAmount() {
        return originalAmount;
    }

    public int getDiscount() {
        return discount;
    }

    public int getTotalAmount() {
        return totalAmount;
    }
} 