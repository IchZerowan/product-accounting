package com.ichzerowan.accounting.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.persistence.*;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Purchase {
    @Id @GeneratedValue(strategy= GenerationType.IDENTITY)
    private Long id;

    private LocalDate date;

    @OneToMany(mappedBy = "purchase", cascade = CascadeType.ALL)
    @JsonIgnoreProperties("purchase")
    private List<PurchaseProduct> products;

    private double total;

    private boolean completed;

    public Purchase() { }

    public Purchase(LocalDate date) {
        this.date = date;
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

    public List<PurchaseProduct> getProducts() {
        return products;
    }

    public void setProducts(List<PurchaseProduct> products) {
        this.products = products;
    }

    public boolean isCompleted() {
        return completed;
    }

    public void setCompleted(boolean completed) {
        this.completed = completed;
    }

    public void updateTotal(){
        total = 0;
        for(PurchaseProduct product: products){
            total += product.getPrice();
        }
    }

    public double getTotal() {
        return total;
    }
}
