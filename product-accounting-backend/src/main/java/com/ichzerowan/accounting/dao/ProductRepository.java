package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.Product;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface ProductRepository extends JpaRepository<Product, Long> {
    public List<Product> findByArchived(boolean archived);
}
