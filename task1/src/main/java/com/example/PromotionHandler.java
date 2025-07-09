package com.example;

public interface PromotionHandler {
    OrderSummary apply(OrderSummary summary, Promotion promotion);
} 