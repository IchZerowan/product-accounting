package com.ichzerowan.accounting.model.delivery;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.ichzerowan.accounting.model.supplier.Supplier;

import javax.persistence.*;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Delivery {
    @Id @GeneratedValue(strategy= GenerationType.IDENTITY)
    private Long id;

    private LocalDate date;
    private double shippingCost;

    @ManyToOne
    @JoinColumn(name = "supplier_id")
    private Supplier supplier;

    @OneToMany(mappedBy = "delivery", cascade = CascadeType.ALL)
    @JsonIgnoreProperties("delivery")
    private List<DeliveryProduct> products;

    private double total;

    private boolean completed;

    public Delivery() { }

    public Delivery(LocalDate date, double shippingCost, Supplier supplier) {
        this.date = date;
        this.shippingCost = shippingCost;
        this.supplier = supplier;
        this.products = new ArrayList<>();
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public LocalDate getDate() {
        return date;
    }

    public void setDate(LocalDate date) {
        this.date = date;
    }

    public Supplier getSupplier() {
        return supplier;
    }

    public void setSupplier(Supplier supplier) {
        this.supplier = supplier;
    }

    public List<DeliveryProduct> getProducts() {
        return products;
    }

    public void setProducts(List<DeliveryProduct> products) {
        this.products = products;
    }

    public double getShippingCost() {
        return shippingCost;
    }

    public void setShippingCost(double shippingCost) {
        this.shippingCost = shippingCost;
    }

    public boolean isCompleted() {
        return completed;
    }

    public void setCompleted(boolean completed) {
        this.completed = completed;
    }

    public void updateTotal(){
        total = shippingCost;
        for(DeliveryProduct product: products){
            total += product.getPrice();
        }
    }

    public double getTotal() {
        return total;
    }
}
