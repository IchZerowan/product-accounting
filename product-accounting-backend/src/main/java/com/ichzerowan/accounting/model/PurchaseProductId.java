package com.ichzerowan.accounting.model;

import java.io.Serializable;
import java.util.Objects;

public class PurchaseProductId implements Serializable {
    private Long purchase;
    private Long product;

    public PurchaseProductId() { }

    public PurchaseProductId(Long purchase, Long product) {
        this.purchase = purchase;
        this.product = product;
    }

    public Long getPurchase() {
        return purchase;
    }

    public void setPurchase(Long purchase) {
        this.purchase = purchase;
    }

    public Long getProduct() {
        return product;
    }

    public void setProduct(Long product) {
        this.product = product;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        PurchaseProductId that = (PurchaseProductId) o;
        return Objects.equals(purchase, that.purchase) && Objects.equals(product, that.product);
    }

    @Override
    public int hashCode() {
        return Objects.hash(purchase, product);
    }
}
