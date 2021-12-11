package com.ichzerowan.accounting.model.purchase;

import java.time.LocalDate;

public class PurchaseDto {
    private LocalDate date;

    private String couponCode;

    public LocalDate getDate() {
        return date;
    }

    public void setDate(LocalDate date) {
        this.date = date;
    }

    public String getCouponCode() {
        return couponCode;
    }

    public void setCouponCode(String couponCode) {
        this.couponCode = couponCode;
    }
}
