package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.supplier.Supplier;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface SupplierRepository extends JpaRepository<Supplier, Long> {
    List<Supplier> findByArchived(boolean archived);
}
