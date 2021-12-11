package com.ichzerowan.accounting.model.transaction;

import javax.persistence.*;
import java.time.LocalDate;

@Entity
public class Transaction {

    public enum Type {
        DELIVERY,
        PURCHASE
    }

    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    private Long id;

    @Enumerated(EnumType.STRING)
    private Type type;

    private Long relId;

    private double amount;

    private LocalDate date;

    public Transaction() { }

    public Transaction(Type type, Long relId, double amount){
        this(type, relId, amount, LocalDate.now());
    }

    public Transaction(Type type, Long relId, double amount, LocalDate date) {
        this.type = type;
        this.relId = relId;
        this.amount = amount;
        this.date = date;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Type getType() {
        return type;
    }

    public void setType(Type type) {
        this.type = type;
    }

    public Long getRelId() {
        return relId;
    }

    public void setRelId(Long relId) {
        this.relId = relId;
    }

    public double getAmount() {
        return amount;
    }

    public void setAmount(double amount) {
        this.amount = amount;
    }

    public LocalDate getDate() {
        return date;
    }

    public void setDate(LocalDate date) {
        this.date = date;
    }
}
