package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.Product;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ProductRepository extends JpaRepository<Product, Long> {

}
