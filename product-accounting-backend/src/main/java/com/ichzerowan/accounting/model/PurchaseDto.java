package com.ichzerowan.accounting.model;

import java.time.LocalDate;

public class PurchaseDto {
    private LocalDate date;

    public LocalDate getDate() {
        return date;
    }

    public void setDate(LocalDate date) {
        this.date = date;
    }
}
