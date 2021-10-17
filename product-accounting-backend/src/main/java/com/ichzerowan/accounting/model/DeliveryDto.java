package com.ichzerowan.accounting.model;

import java.time.LocalDate;

public class DeliveryDto {
    private Long supplierId;
    private LocalDate date;

    public Long getSupplierId() {
        return supplierId;
    }

    public void setSupplierId(Long supplierId) {
        this.supplierId = supplierId;
    }

    public LocalDate getDate() {
        return date;
    }

    public void setDate(LocalDate date) {
        this.date = date;
    }
}
