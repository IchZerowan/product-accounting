package com.ichzerowan.accounting.model;

import javax.persistence.*;

@Entity
@Table(name = "delivery_product")
@IdClass(DeliveryProductId.class)
public class DeliveryProduct {
    @Id
    @ManyToOne
    @JoinColumn(name = "delivery_id")
    private Delivery delivery;

    @Id
    @ManyToOne
    @JoinColumn(name = "product_id")
    private Product product;

    private int count;

    public DeliveryProduct() { }

    public DeliveryProduct(Delivery delivery, Product product, int count){
        this.delivery = delivery;
        this.product = product;
        this.count = count;
    }

    public Delivery getDelivery() {
        return delivery;
    }

    public void setDelivery(Delivery delivery) {
        this.delivery = delivery;
    }

    public int getCount() {
        return count;
    }

    public void setCount(int count) {
        this.count = count;
    }

    public Product getProduct() {
        return product;
    }

    public void setProduct(Product product) {
        this.product = product;
    }
}