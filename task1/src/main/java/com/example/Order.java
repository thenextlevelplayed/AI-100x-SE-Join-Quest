package com.example;

import java.util.List;

public class Order {
    private List<OrderItem> orderItems;

    public Order(List<OrderItem> orderItems) {
        this.orderItems = orderItems;
    }

    public int getOriginalAmount() {
        return orderItems.stream()
                .mapToInt(OrderItem::getSubtotal)
                .sum();
    }

    public List<OrderItem> getOrderItems() {
        return orderItems;
    }
} 