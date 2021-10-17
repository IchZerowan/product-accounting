package com.ichzerowan.accounting;

import com.ichzerowan.accounting.dao.DeliveryProductRepository;
import com.ichzerowan.accounting.dao.DeliveryRepository;
import com.ichzerowan.accounting.dao.ProductRepository;
import com.ichzerowan.accounting.dao.SupplierRepository;
import com.ichzerowan.accounting.model.Delivery;
import com.ichzerowan.accounting.model.DeliveryProduct;
import com.ichzerowan.accounting.model.Product;
import com.ichzerowan.accounting.model.Supplier;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.util.Date;

@SpringBootApplication
public class ProductAccountingApplication {

	private static final Logger log = LoggerFactory.getLogger(ProductAccountingApplication.class);

	public static void main(String[] args) {
		SpringApplication.run(ProductAccountingApplication.class, args);
	}

	@Autowired
	private ProductRepository productRepository;

	@Autowired
	private SupplierRepository supplierRepository;

	@Autowired
	private DeliveryRepository deliveryRepository;

	@Autowired
	private DeliveryProductRepository deliveryProductRepository;

	@Bean
	CommandLineRunner initDeliveries(){
		return args -> {
			Product product1 = new Product("Biscuits", "Just some pretty biscuits", 16, 12);
			Product product2 = new Product("Sugar", "A must-have component for any meal", 5, 3);
			productRepository.save(product1);
			productRepository.save(product2);

			Supplier supplier = new Supplier("Bakery A ltd.", "+380999999999");
			supplierRepository.save(supplier);

			Delivery delivery = new Delivery(LocalDate.parse("2017-11-15"), supplier);

			DeliveryProduct deliveryProduct1 = new DeliveryProduct(delivery, product1, 10);
			DeliveryProduct deliveryProduct2 = new DeliveryProduct(delivery, product2, 20);

			log.info("Preloading " + deliveryRepository.save(delivery));

			log.info("Preloading " + deliveryProductRepository.save(deliveryProduct1));
			log.info("Preloading " + deliveryProductRepository.save(deliveryProduct2));
		};
	}
}
