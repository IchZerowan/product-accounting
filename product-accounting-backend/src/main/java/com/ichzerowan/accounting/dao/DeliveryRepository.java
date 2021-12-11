package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.delivery.Delivery;
import org.springframework.data.jpa.repository.JpaRepository;

public interface DeliveryRepository extends JpaRepository<Delivery, Long> {

}
