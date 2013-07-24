CREATE TABLE linkme_owner.order_invoice (
	id VARCHAR(50) NOT NULL,
	ownerId VARCHAR(50) NOT NULL,
	invoiceDate DateTime NULL,
	numberOfResume VARCHAR(20) NULL,
	pricePerResume VARCHAR(20) NULL,
	total VARCHAR(20) NULL,
	type VARCHAR(1)

	CONSTRAINT pk_order_invoice PRIMARY KEY (id)
)

CREATE INDEX i_order_invoice_Id ON linkme_owner.order_invoice (id)

CREATE TABLE linkme_owner.resume_purchase_details (
	id VARCHAR(50) NOT NULL,
	numberOfResume VARCHAR(20) NULL,
	pricePerResume VARCHAR(20) NULL

	CONSTRAINT pk_resume_purchase_details PRIMARY KEY (id)
)

CREATE TABLE linkme_owner.credit_card_purchase_details (
	id VARCHAR(50) NOT NULL,
	invoiceNumber VARCHAR(50) NULL,
	gst VARCHAR(20) NULL

	CONSTRAINT pk_credit_card_purchase_details PRIMARY KEY (id)
)

CREATE TABLE linkme_owner.email_purchase_details (
	id VARCHAR(50) NOT NULL,
	numberOfInNetworker VARCHAR(20) NULL,
	numberOfOutNetworker VARCHAR(20) NULL,
	pricePerOutNetworker VARCHAR(20) NULL

	CONSTRAINT pk_email_purchase_details PRIMARY KEY (id)
)

CREATE TABLE linkme_owner.credit_purchase_details (
	id VARCHAR(50) NOT NULL,
	remainingCredits VARCHAR(20) NULL
	
	CONSTRAINT pk_credit_purchase_details PRIMARY KEY (id)
)
