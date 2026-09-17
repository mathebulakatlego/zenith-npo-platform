import Navbar from './components/layout/Navbar'
import Footer from './components/layout/Footer'
import PageContainer from './components/layout/PageContainer'
import Section from './components/layout/Section'
import './App.css'

function App() {
  return (
    <>
      <Navbar />

      <main>
        <section className="home-hero">
          <PageContainer>
            <div className="home-hero__content">
              <p className="home-hero__eyebrow">
                Driven By Purpose | Powered By Technology
              </p>

              <h1>Building skills. Creating opportunities. Shaping futures.</h1>

              <p className="home-hero__text">
                Zenith Future Skills Hub equips young people with practical
                digital skills, career readiness and opportunities to grow.
              </p>

              <div className="home-hero__actions">
                <a className="btn btn--primary" href="/apply">
                  Apply Now
                </a>

                <a className="btn btn--secondary" href="/programmes">
                  Explore Programmes
                </a>
              </div>
            </div>
          </PageContainer>
        </section>

        <Section>
          <PageContainer>
            <div className="home-intro">
              <div>
                <p className="section-eyebrow">Who We Are</p>
                <h2>Skills that open doors.</h2>
              </div>

              <div>
                <p>
                  Zenith Future Skills Hub is focused on helping young people
                  develop practical skills that can translate into meaningful
                  opportunities.
                </p>

                <p>
                  Through accessible training, mentorship and career
                  preparation, we help learners move from potential to
                  possibility.
                </p>
              </div>
            </div>
          </PageContainer>
        </Section>

        <Section className="section--light">
          <PageContainer>
            <div className="section-heading">
              <p className="section-eyebrow">Our Programme</p>
              <h2>Learn skills you can use.</h2>

              <p>
                Practical learning designed to build confidence, capability
                and career readiness.
              </p>
            </div>

            <div className="programme-highlight">
              <div>
                <h3>Full Stack Developer Bootcamp</h3>

                <p>
                  A practical programme covering computer literacy, Git and
                  Linux, HTML, CSS, SQL, JavaScript, Python, AI literacy and
                  career readiness.
                </p>
              </div>

              <a className="btn btn--primary" href="/programmes">
                View Programme
              </a>
            </div>
          </PageContainer>
        </Section>

        <Section className="home-cta">
          <PageContainer>
            <div className="home-cta__content">
              <p className="section-eyebrow">Your Next Step</p>

              <h2>Ready to grow your skills?</h2>

              <p>
                Explore our programmes and discover where your next opportunity
                could begin.
              </p>

              <a className="btn btn--primary" href="/apply">
                Start Your Application
              </a>
            </div>
          </PageContainer>
        </Section>
      </main>

      <Footer />
    </>
  )
}

export default App
