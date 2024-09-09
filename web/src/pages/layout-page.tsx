
import { Card, Container, Nav, Navbar } from 'react-bootstrap';
import { Outlet } from 'react-router-dom';
import NavbarMain from '../components/navbar/navbar';
import { Colors } from '../constants/constants';

interface ILayoutPage {
  children?: React.ReactNode;
}
export const LayoutPage: React.FunctionComponent<ILayoutPage> = props => {
  // const {children} = props;

  return (
    <div className=''>
      <NavbarMain />
      {/* <div id="myCarousel" className="carousel slide" data-bs-ride="carousel" data-bs-touch={true} data-bs-interval={false}>
        <div className="carousel-indicators">
          <button type="button" data-bs-target="#myCarousel" data-bs-slide-to="0" className="" aria-label="Slide 1"></button>
          <button type="button" data-bs-target="#myCarousel" data-bs-slide-to="1" aria-label="Slide 2" className=""></button>
          <button type="button" data-bs-target="#myCarousel" data-bs-slide-to="2" aria-label="Slide 3" className="active" aria-current="true"></button>
        </div>
        <div className="carousel-inner">
          <div className="carousel-item">
            <svg className="bd-placeholder-img" width="100%" height="100%" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" preserveAspectRatio="xMidYMid slice" focusable="false"><rect width="100%" height="100%" fill="#777"></rect></svg>

            <div className="container">
              <div className="carousel-caption text-center">
                <h1>Example headline.</h1>
                <p>Some representative placeholder content for the first slide of the carousel.</p>
                <p><a className="btn btn-lg btn-primary" href="#">Sign up today</a></p>
              </div>
            </div>
          </div>
          <div className="carousel-item">
            <svg className="bd-placeholder-img" width="100%" height="100%" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" preserveAspectRatio="xMidYMid slice" focusable="false"><rect width="100%" height="100%" fill="#777"></rect></svg>

            <div className="container">
              <div className="carousel-caption">
                <h1>Another example headline.</h1>
                <p>Some representative placeholder content for the second slide of the carousel.</p>
                <p><a className="btn btn-lg btn-primary" href="#">Learn more</a></p>
              </div>
            </div>
          </div>
          <div className="carousel-item active">
            <svg className="bd-placeholder-img" width="100%" height="100%" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" preserveAspectRatio="xMidYMid slice" focusable="false"><rect width="100%" height="100%" fill="#777"></rect></svg>

            <div className="container">
              <div className="carousel-caption text-center">
                <h1>One more for good measure.</h1>
                <p>Some representative placeholder content for the third slide of this carousel.</p>
                <p><a className="btn btn-lg btn-primary" href="#">Browse gallery</a></p>
              </div>
            </div>
          </div>
        </div>
        <button className="carousel-control-prev" type="button" data-bs-target="#myCarousel" data-bs-slide="prev">
          <span className="carousel-control-prev-icon" aria-hidden="true"></span>
          <span className="visually-hidden">Previous</span>
        </button>
        <button className="carousel-control-next" type="button" data-bs-target="#myCarousel" data-bs-slide="next">
          <span className="carousel-control-next-icon" aria-hidden="true"></span>
          <span className="visually-hidden">Next</span>
        </button>
      </div> */}
      <div className='h-100 LayoutCustom' style={{ backgroundColor: Colors.MMYellow }}>
        <Container className='h-100' >
          <Card className='border-0 d-flex justify-content-center' style={{ backgroundColor: Colors.MMYellow }} >
            {/* <CardBody className='border-0 '> */}
            <div className='d-flex justify-content-center'>
              <Outlet />
            </div>
            {/* </CardBody> */}
          </Card>
        </Container>
      </div>
    </div>

  );
}