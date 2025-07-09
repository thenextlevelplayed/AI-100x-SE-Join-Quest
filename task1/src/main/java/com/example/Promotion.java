package com.example;

public class Promotion {
    private String type;
    private int threshold;
    private int discount;
    private String targetCategory;

    public Promotion(String type, int threshold, int discount, String targetCategory) {
        this.type = type;
        this.threshold = threshold;
        this.discount = discount;
        this.targetCategory = targetCategory;
    }

    public String getType() {
        return type;
    }

    public int getThreshold() {
        return threshold;
    }

    public int getDiscount() {
        return discount;
    }

    public String getTargetCategory() {
        return targetCategory;
    }
} 