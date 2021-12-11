package com.ichzerowan.accounting.model;

import javax.persistence.*;

@Entity
@Table(name = "purchase_product")
@IdClass(PurchaseProductId.class)
public class PurchaseProduct {
    @Id
    @ManyToOne
    @JoinColumn(name = "purchase_id")
    private Purchase purchase;

    @Id
    @ManyToOne
    @JoinColumn(name = "product_id")
    private Product product;

    private int count;

    private double price;

    public PurchaseProduct() { }

    public PurchaseProduct(Purchase purchase, Product product, int count){
        this.purchase = purchase;
        this.product = product;
        setCount(count);
    }

    public int getCount() {
        return count;
    }

    public void setCount(int count) {
        this.count = count;
        this.price = count * product.getPriceWholesale();
    }

    public Purchase getPurchase() {
        return purchase;
    }

    public void setPurchase(Purchase purchase) {
        this.purchase = purchase;
    }

    public Product getProduct() {
        return product;
    }

    public void setProduct(Product product) {
        this.product = product;
    }

    public double getPrice() {
        return price;
    }
}