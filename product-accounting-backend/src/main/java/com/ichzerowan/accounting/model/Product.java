package com.ichzerowan.accounting.model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
public class Product {
    @Id @GeneratedValue(strategy= GenerationType.IDENTITY)
    private Long id;

    private String name;
    private String description;
    private double priceRetail;
    private double priceWholesale;
    private boolean archived;

    private int count;

    public Product() { }

    public Product(String name, String description, double priceRetail, double priceWholesale) {
        this.name = name;
        this.description = description;
        this.priceRetail = priceRetail;
        this.priceWholesale = priceWholesale;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public boolean isArchived() {
        return archived;
    }

    public void setArchived(boolean archived) {
        this.archived = archived;
    }

    public double getPriceRetail() {
        return priceRetail;
    }

    public void setPriceRetail(double priceRetail) {
        this.priceRetail = priceRetail;
    }

    public double getPriceWholesale() {
        return priceWholesale;
    }

    public void setPriceWholesale(double priceWholesale) {
        this.priceWholesale = priceWholesale;
    }

    public int getCount() {
        return count;
    }

    public void setCount(int count) {
        this.count = count;
    }
}
