-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 01, 2018 at 01:29 PM
-- Server version: 10.1.30-MariaDB
-- PHP Version: 7.2.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `binan_kiosk`
--

-- --------------------------------------------------------

--
-- Table structure for table `departments`
--

CREATE TABLE `departments` (
  `department_id` int(4) NOT NULL,
  `department_name` text NOT NULL,
  `Dep_description` text NOT NULL,
  `Department_image_path` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `departments`
--

INSERT INTO `departments` (`department_id`, `department_name`, `Dep_description`, `Department_image_path`) VALUES
(1, 'OFFICE OF THE CITY COOPERATIVES', 'Maintains office services by organizing office operations and procedures; preparing payroll; controlling correspondence; designing filing systems; reviewing and approving supply requisitions; assigning and monitoring clerical functions.', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(2, 'OFFICE OF THE CITY MAYOR', 'Policy development and coordination. Directing and monitoring all City services focusing on efficient and responsive delivery of those services. Directing management of the City\'s fiscal policy.', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(3, 'OFFICE OF THE CITY ASSESSOR', 'Determines the value of a property for local real estate taxation purposes', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(4, 'OFFICE OF THE CITY CIVIL REGISTRAR', 'Records the vital events (births, marriages, permanent, compulsory and universal recording of the occurrence and characteristics of vital events ', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(5, 'OFFICE OF THE CHRD', 'To provide quality personnel and implement a comprehensive and balanced human resource development program through implementation of rules and policies on recruitment, appointment and separation processes, personnel benefits, training, skills.', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(6, 'OFFICE OF THE CITY INFORMATION OFFICER', 'Supervise and coordinate administrative functions, personnel and general services', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(9, 'OFFICE OF THE CITY POPULATION ', 'Addresses population issues and concerns with the fundamental view that the management of population concerns is a critical factor in development and that meeting the aspirations of the Filipino family to improve overall welfare is a key motivating force underlying the dynamics between population and development. It is concerned with the aim of achieving an improved quality of life for all members of the community by managing growth and distribution of population.', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(10, 'PUBLIC EMPLOYMENT SERVICE OFFICE', 'Ensure the prompt, timely and efficient delivery of employment service and provision of information on the other DOLE programs.', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(12, 'OFFICE OF THE CITY VICE MAYOR', 'Sign all warrants drawn on the city or municipal treasury for all expenditures appropriated for the operation of the Sangguniang Panlungsod or Sangguniang Bayan.\r\n\r\nSubject to civil service law, rules and regulations, appoint all officials and employees of the Sangguniang Panlungsod or Sangguniang Bayan, except those whose manner of appointment is specifically provided in R.A. No. 7610', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(14, 'OFFICE OF THE CITY ENGINEER', 'The Office of the City Engineer is responsible for the all infrastructures, public works, and other engineering matters in the City Government of Binan.', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(15, 'OFFICE OF THE CITY GENERAL SERVICES OFFICER', 'Develop plans and strategies related to general services.  Provides leadership and best practices in managing facilities, supplies, security and resource conservation, maintenance, and other support services to elected officials.', 'C:\\WebApps\\Images/Departments/binanlogo.png'),
(16, 'Others', 'Related to other stuffs', 'C:\\WebApps\\Images/Departments/binanlogo.png');

-- --------------------------------------------------------

--
-- Table structure for table `jobs`
--

CREATE TABLE `jobs` (
  `job_id` int(11) NOT NULL,
  `job_name` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `jobs`
--

INSERT INTO `jobs` (`job_id`, `job_name`) VALUES
(1, 'Advertising Jobs'),
(2, 'Agriculture'),
(3, 'Architect/Interior Design'),
(4, 'Electrical Engineering'),
(5, 'Entertainment Jobs'),
(6, 'Civil Engineering'),
(7, 'Secretarial'),
(8, 'Hotel/Tourism'),
(9, 'Merchandising'),
(10, 'Restaurant'),
(11, 'Retail Sales'),
(12, 'Manufacturing'),
(13, 'Real Estate'),
(14, 'Education '),
(15, 'Customer Service'),
(16, 'IT'),
(17, 'Banking/Financial'),
(18, 'Oil/Gas Engineering'),
(19, 'Industrial Engineering'),
(20, 'Personal Care'),
(21, 'Others');

-- --------------------------------------------------------

--
-- Table structure for table `jobtypes`
--

CREATE TABLE `jobtypes` (
  `job_typeID` int(4) NOT NULL,
  `job_types` varchar(40) NOT NULL,
  `job_id` int(11) NOT NULL,
  `job_description` text NOT NULL,
  `job_location` varchar(70) NOT NULL,
  `job_company` varchar(70) NOT NULL,
  `job_image_path` varchar(100) NOT NULL,
  `Logo_image_path` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `jobtypes`
--

INSERT INTO `jobtypes` (`job_typeID`, `job_types`, `job_id`, `job_description`, `job_location`, `job_company`, `job_image_path`, `Logo_image_path`) VALUES
(1111, 'Sales Representative', 1, 'Develops sales strategies for a company. Focus on sales, and marketing activities. Supports the marketing department by carrying out the daily tasks that keep the department functioning and facilitate the duties of the marketing manager. Arrange meetings/ appointments for prospect..', 'Binan, Laguna', ' SOUTHERN ARMS CORPORATION ', 'test.jpg', 'test.jpg'),
(1122, 'Gardener', 2, 'Candidate must possess at least a High School Diploma, Vocational Diploma / Short Course Certificate, any field. Candidate must know how to propagate ornamental plants. No work experience required. Full-Time position(s) available.', 'Laguna (Calabarzon & Mimaropa)', 'CALIRAYA RESORT CLUB\r\n', '1122.jpg', 'man-two.jpg'),
(1123, 'Senior Electrical Engineers (DESIGN)', 4, 'Electrical design experience, Feeder line system, Substation design, High &amp; low voltage, With experience either in hotel project or semi-con plant or commercial buildings, Building codes and other electrical design standards, Computer literate,', 'Manila City - Malate', ' WEL Contracting Corp.', 'one.png', 'three.png'),
(1126, 'Store Planning Secretary', 7, 'Candidate must possess at least Bachelor\'s/College Degree in Business Studies/Administration/Management or equivalent. Fresh graduate/Experienced...', 'Pasig City (National Capital Reg)', 'I-FASHION MKTG. CO.', 'test.jpg', 'test.jpg'),
(1127, 'Stockman (Farm Supervisor)', 2, 'Conducts, facilitates, and evaluates 5 days immersion for newly-hired Store Crew in an accredited Training Hub.', 'Binan, Laguna', 'Bounty Fresh Food Inc.', 'test.jpg', 'test.jpg'),
(1128, 'Bounty Fresh Food Inc.', 2, 'Schedules feed mills activities to include feed manufacturing and shipments.', 'Pasay City, Manila', 'Bounty Fresh Food Inc.', 'test.jpg', 'test.jpg'),
(1130, 'Credit Card Field Sales Officer ', 11, 'Responsible for acquiring complete New-To-Bank (NTB) credit card applications that will convert to approved applications.', 'Metro Manila', 'UnionBank', 'test.jpg', 'test.jpg'),
(1131, 'Sales Representative', 1, 'dffdsfasdfafdsfasfasfdfdsfasfsdfsafsfddasfsdfsdfsdfsafsafdasfasfsafafa', 'Binan, Laguna', 'GrafixArts', 'test.jpg', 'test.jpg'),
(1133, 'Village Architect', 3, 'To ensure that on-going house construction and existing houses within the village are compliance with the Village Construction Rules...', 'Cavite (Calabarzon & Mimaropa) ', 'AYALA WESTGROVE HEIGHTS HOMEOWNERS ASSOCIATION INC.', 'test.jpg', 'test.jpg'),
(1134, 'Hotel Operations Supervisor', 8, 'Oversee day-to-day work performance of Hotel Staff; Monitor team performance and report on metrics<br>&bull; Delegate tasks, set deadlines and prepare work schedule; Discover training needs and provide coaching', 'Cebu (Cebu City)', 'Temps and Staffers Inc.', 'test.jpg', 'test.jpg'),
(1135, 'Animal Nutritionist', 2, 'Formulates good quality animal diets for sales. Accurately encodes feeds formula into Feed Mill ', 'Taguig City, Manila', 'Bounty Fresh Food Inc.', 'test.jpg', 'test.jpg'),
(1136, 'Fertilizer Specialist', 2, 'The fertilizer specialist will be responsible in formulating manure-based fertilizers that are made specifically for the soil nutrient requirements of agricultural lands of a particular customer.', 'Taguig City, Manila', 'Bounty Fresh Food Inc.', 'test.jpg', 'test.jpg'),
(1137, 'Category Merchandise Manager', 9, 'Responsible for ordering of merchandise for the department/branch and monitors them thereafter. Ensures availability of stocks proportionate to merchandise movements. Review and analyzes sales reports and updates the same based on changes in business/market trends. Ensures that all..', 'MOA Complex Pasay City', 'CASAMIA FURNITURE CENTER, INC.', 'test.jpg', 'test.jpg'),
(1144, 'Beauty Advisor -Clinique', 20, 'Lauder Beauty Advisors set the standard for excellence in the cosmetics industry by providing exceptional customer service and achieving sales goals on a daily basis', 'Brgy. San Antonio, Makati City', 'Topserve Service Solutions, Inc.', 'test.jpg', 'test.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `offices`
--

CREATE TABLE `offices` (
  `office_id` int(11) NOT NULL,
  `office_name` text NOT NULL,
  `room_name` varchar(20) NOT NULL,
  `department_id` int(11) NOT NULL,
  `offices_image_path` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `offices`
--

INSERT INTO `offices` (`office_id`, `office_name`, `room_name`, `department_id`, `offices_image_path`) VALUES
(1, 'Treasury Office', 'r102', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(2, 'Local Civil Registry Office', 'r101', 4, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(3, 'City Social Welfare Development Office', 'r103', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(4, 'Office of the Senior Citizen Affairs', 'r104', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(5, 'CNA', 'r105', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(6, 'Chapel', 'r106', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(7, 'Negosyo Center', 'r107', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(8, 'BPAO', 'r108', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(9, 'City Environment and Natural Resources Office', 'r109', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(10, 'No Office', 'r110', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(11, 'Business and Licensing Office', 'r111', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(12, 'Assessors Office', 'r112', 3, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(13, 'Accounting Office', 'r201', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(14, 'Auditors Office', 'r202', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(15, 'City Local Government Operations Office (DILG)', 'r203', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(16, 'Office of the City Council Secretary', 'r204', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(17, 'Hon. Liza L. Cardeño', 'r205', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(18, 'Hon. Dada A. Reyes', 'r206', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(19, 'Hon. Alexis Echit Desuasido', 'r207', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(20, 'Office of the City Vice Mayor', 'r208', 12, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(21, 'Ambrosio Rianzares Bautista Hall', 'r209', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(22, 'ABC President Office', 'r210', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(23, 'Hon. Rommel R. Dicdican(ABC)', 'r211', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(24, 'Hon. Wilfredo I. Bejasa Jr.', 'r212', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(25, 'Hon. Jaime N. Salandanan', 'r213', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(26, 'Hon. Jayson A. Souza', 'r214', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(27, 'Hon. Alvin Z. Garcia', 'r215', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(28, 'Hon. Flaviano D. Pecaña Jr.', 'r216', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(29, 'Hon. Gener D. Romantigue', 'r217', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(30, 'Hon. Monching C. Carrillo', 'r218', 6, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(31, 'Hon. Mariz Lindsey V. Tan Gana', 'r219', 10, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(32, 'Hon. Donna Angela P. Yatco', 'r220', 1, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(33, 'City Public Information Office', 'r221', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(34, 'City Budget Office', 'r301', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(35, 'City Population Office', 'r302', 9, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(36, 'Human Resources Development Office', 'r303', 5, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(37, 'Gender And Development Office', 'r304', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(38, 'City Community Affairs Office', 'r305', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(39, 'City Legal Office', 'r306', 15, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(40, 'General Services Office', 'r307', 2, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(41, 'Office of the City Mayor', 'r308', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(42, 'Serbisyong Arman Hall', 'r309', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(43, 'Information and Communications Technology Office', 'r310', 14, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(44, 'City Planning and Development Office', 'r311', 14, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(45, 'Public Employment Service Office', 'r222', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(46, 'Conference Room', 'r223', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg'),
(47, 'Engineering Office', 'r312', 16, 'C:/WebApps/Images/Offices/Binan_City_Hall.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `officials`
--

CREATE TABLE `officials` (
  `officials_id` int(4) NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `middle_initial` text NOT NULL,
  `suffex` text NOT NULL,
  `position_id` int(11) NOT NULL,
  `office_id` int(11) NOT NULL,
  `image_path` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `officials`
--

INSERT INTO `officials` (`officials_id`, `first_name`, `last_name`, `middle_initial`, `suffex`, `position_id`, `office_id`, `image_path`) VALUES
(1001, 'Ramon', 'Almazan', 'A.', '', 11, 43, 'Fastfood.PNG'),
(1056, 'Ronaldo', 'Roscain', 'G.', '', 4, 2, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg'),
(1070, 'Walfredo', 'Dimaguila ', 'R.', 'Jr.', 12, 3, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg'),
(1160, 'Raul', 'Villanueva', 'D.', '', 2, 4, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg'),
(1214, 'Tomas Ignacio', 'Manabat', 'C.', '', 3, 5, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg'),
(1221, 'Jenny Anne', 'Sarmiento', 'B.', '', 10, 6, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg'),
(1232, 'Wilfredo', 'Alintanahin', 'F.', '', 5, 7, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg'),
(1255, 'Gerardo', 'Santos', 'L.', '', 6, 8, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg'),
(1377, 'Roman', 'Carencia', 'E.', '', 11, 9, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg'),
(1394, 'Maria', 'Bonacua', 'R.', '', 15, 10, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg'),
(1401, 'Romualdo', 'Garcia', 'A.', '', 7, 11, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg'),
(1671, 'Angelo', 'Alonte', 'B.', '', 14, 12, 'C:\\WebApps\\Images/Officials/Ramon Almazan.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `positions`
--

CREATE TABLE `positions` (
  `position_id` int(11) NOT NULL,
  `position_name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `positions`
--

INSERT INTO `positions` (`position_id`, `position_name`) VALUES
(2, 'CITY ASSESSOR'),
(3, 'CITY CIVIL REGISTRAR'),
(4, 'CITY COOPERATIVE OFFICER'),
(5, 'CITY ENGINEER '),
(6, 'CITY GENERAL SERVICES OFFICER '),
(7, 'CITY GOV\'T. DEPT. HEAD I'),
(10, 'CITY HRD'),
(11, 'CITY INFORMATION OFFICER'),
(12, 'CITY MAYOR'),
(14, 'CITY VICE MAYOR'),
(15, 'DEPT. HEAD');

-- --------------------------------------------------------

--
-- Table structure for table `services`
--

CREATE TABLE `services` (
  `service_id` int(11) NOT NULL,
  `service_name` text NOT NULL,
  `image_name` varchar(100) NOT NULL,
  `office_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `services`
--

INSERT INTO `services` (`service_id`, `service_name`, `image_name`, `office_id`) VALUES
(1, 'Payment of Real Property Transfer Tax', 'Payment of Real Property Transfer Tax.jpg', 1),
(2, 'Issuance of Community Tax Certificate for Corporation', 'Issuance of Community Tax Certificate for Corporation.jpg', 1),
(3, 'Issuance of Professional Tax Reciept', 'Issuance of Professional Tax Reciept.jpg', 1),
(4, 'Payment of Real Property Tax', 'Payment of Real Property Tax.jpg', 1),
(5, 'Payment of Business Tax', 'Payment of Business Tax.jpg', 1),
(6, 'Certification of Tax Clearance', 'Certification of Tax Clearance.jpg', 1),
(7, 'Payment of Burial', 'Payment of Burial.jpg', 1),
(8, 'Payment of Building Permit', 'Payment of Building Permit.jpg', 1),
(9, 'Payment of Police, Mayor, and Other Certificate Clearance', 'Payment of Police, Mayor, and Other Certificate Clearance.jpg', 1),
(10, 'Payment of Civil Registration', 'Payment of Civil Registration.jpg', 1),
(11, 'Payment of Traffic Violation', 'Payment of Traffice Violation.jpg', 1),
(12, 'Issuance of Community Tax for Individual', 'Issuance of Community Tax Certificate for Corporation.jpg', 1),
(13, 'Payment of Weight and Measure License Fees', 'Payment of Weight and Measure License Fees.jpg', 1),
(14, 'Payment of Market Stalls and Electrical Fees', 'Payment of Market Stalls and Electrical Fees.jpg', 1);

-- --------------------------------------------------------

--
-- Table structure for table `slider_images`
--

CREATE TABLE `slider_images` (
  `image_id` int(10) NOT NULL,
  `image_name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `slider_images`
--

INSERT INTO `slider_images` (`image_id`, `image_name`) VALUES
(1, 'Free Movies.jpg'),
(2, 'Iskolar.jpg'),
(3, 'Job Fair.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `username` varchar(20) NOT NULL,
  `password` varchar(50) NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `middle_initial` text NOT NULL,
  `position` text NOT NULL,
  `department` text NOT NULL,
  `email` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`username`, `password`, `first_name`, `last_name`, `middle_initial`, `position`, `department`, `email`) VALUES
('admin', 'admin', 'admin', 'admin', 'admin', 'Programmer', 'ICT', 'gilboi@hotmale.com'),
('klcaraon', '1234567890', 'Kenneth', 'Caraon', 'L', 'Programmer', 'ICT', 'klcaraon@gmail.com');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `departments`
--
ALTER TABLE `departments`
  ADD PRIMARY KEY (`department_id`);

--
-- Indexes for table `jobs`
--
ALTER TABLE `jobs`
  ADD PRIMARY KEY (`job_id`);

--
-- Indexes for table `jobtypes`
--
ALTER TABLE `jobtypes`
  ADD PRIMARY KEY (`job_typeID`),
  ADD KEY `job_id` (`job_id`);

--
-- Indexes for table `offices`
--
ALTER TABLE `offices`
  ADD PRIMARY KEY (`office_id`),
  ADD KEY `department_id` (`department_id`);

--
-- Indexes for table `officials`
--
ALTER TABLE `officials`
  ADD PRIMARY KEY (`officials_id`),
  ADD KEY `position_id` (`position_id`),
  ADD KEY `office_id` (`office_id`);

--
-- Indexes for table `positions`
--
ALTER TABLE `positions`
  ADD PRIMARY KEY (`position_id`);

--
-- Indexes for table `services`
--
ALTER TABLE `services`
  ADD PRIMARY KEY (`service_id`),
  ADD KEY `office_id` (`office_id`);

--
-- Indexes for table `slider_images`
--
ALTER TABLE `slider_images`
  ADD PRIMARY KEY (`image_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `departments`
--
ALTER TABLE `departments`
  MODIFY `department_id` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `jobs`
--
ALTER TABLE `jobs`
  MODIFY `job_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `offices`
--
ALTER TABLE `offices`
  MODIFY `office_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=48;

--
-- AUTO_INCREMENT for table `positions`
--
ALTER TABLE `positions`
  MODIFY `position_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `services`
--
ALTER TABLE `services`
  MODIFY `service_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `slider_images`
--
ALTER TABLE `slider_images`
  MODIFY `image_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `jobtypes`
--
ALTER TABLE `jobtypes`
  ADD CONSTRAINT `jobtypes_ibfk_1` FOREIGN KEY (`job_id`) REFERENCES `jobs` (`job_id`);

--
-- Constraints for table `offices`
--
ALTER TABLE `offices`
  ADD CONSTRAINT `offices_ibfk_1` FOREIGN KEY (`department_id`) REFERENCES `departments` (`department_id`);

--
-- Constraints for table `officials`
--
ALTER TABLE `officials`
  ADD CONSTRAINT `officials_ibfk_1` FOREIGN KEY (`position_id`) REFERENCES `positions` (`position_id`),
  ADD CONSTRAINT `officials_ibfk_2` FOREIGN KEY (`office_id`) REFERENCES `offices` (`office_id`);

--
-- Constraints for table `services`
--
ALTER TABLE `services`
  ADD CONSTRAINT `services_ibfk_1` FOREIGN KEY (`office_id`) REFERENCES `offices` (`office_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
