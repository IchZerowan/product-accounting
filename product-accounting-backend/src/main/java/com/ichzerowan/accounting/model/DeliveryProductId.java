package com.ichzerowan.accounting.model;

import java.io.Serializable;
import java.util.Objects;

public class DeliveryProductId implements Serializable {
    private Long delivery;
    private Long product;

    public DeliveryProductId() { }

    public DeliveryProductId(Long delivery, Long product) {
        this.delivery = delivery;
        this.product = product;
    }

    public Long getProduct() {
        return product;
    }

    public void setProduct(Long product) {
        this.product = product;
    }

    public Long getDelivery() {
        return delivery;
    }

    public void setDelivery(Long delivery) {
        this.delivery = delivery;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        DeliveryProductId that = (DeliveryProductId) o;
        return Objects.equals(delivery, that.delivery) && Objects.equals(product, that.product);
    }

    @Override
    public int hashCode() {
        return Objects.hash(delivery, product);
    }
}
