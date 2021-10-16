package com.ichzerowan.accounting;

import com.ichzerowan.accounting.dao.ProductRepository;
import com.ichzerowan.accounting.model.Product;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class ProductAccountingApplication {

	private static final Logger log = LoggerFactory.getLogger(ProductAccountingApplication.class);

	public static void main(String[] args) {
		SpringApplication.run(ProductAccountingApplication.class, args);
	}

	@Bean
	CommandLineRunner initDatabase(ProductRepository repository){
		return args -> {
			log.info("Preloading " + repository.save(new Product("Biscuits", "Just some pretty biscuits", 16)));
			log.info("Preloading " + repository.save(new Product("Sugar", "A must-have component for any meal", 5)));
		};
	}
}
