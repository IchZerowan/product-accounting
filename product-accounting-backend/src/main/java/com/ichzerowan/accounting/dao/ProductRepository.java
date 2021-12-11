package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.product.Product;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface ProductRepository extends JpaRepository<Product, Long> {
    List<Product> findByArchived(boolean archived);
}
