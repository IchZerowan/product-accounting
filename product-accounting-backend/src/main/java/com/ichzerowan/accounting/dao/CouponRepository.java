package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.coupon.Coupon;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface CouponRepository extends JpaRepository<Coupon, Long> {
    List<Coupon> findByArchived(boolean archived);
    Coupon findFirstByCode(String code);
}
